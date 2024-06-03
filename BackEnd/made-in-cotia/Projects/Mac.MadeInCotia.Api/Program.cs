using Mac.MadeInCotia.Data.Context;
using MAC.MadeInCotia.Biz.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Program
{
    public static void Main(string[] args)

    {
        //This line creates a WebApplicationBuilder instance, initializing it with default configurations including configuration sources like appsettings.json and appsettings.
        var builder = WebApplication.CreateBuilder(args);
        var key = Encoding.ASCII.GetBytes("MIC@willjwt");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
        {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<EmailService>();
        builder.Services.AddScoped<PermissoesService>();
        builder.Services.AddScoped<TelefoneService>();
        builder.Services.AddScoped<ColaboradorService>();
        builder.Services.AddScoped<LoginService>();


        // Database context
        builder.Services.AddDbContext<MacMadeInCotiaContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = "swagger";  // This sets the URL path for Swagger UI
        });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}