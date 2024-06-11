using Mac.MadeInCotia.Data.Context;
using MAC.MadeInCotia.Biz.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

public class Program
{
    public static void Main(string[] args)

    {
        //This line creates a WebApplicationBuilder instance, initializing it with default configurations including configuration sources like appsettings.json and appsettings.
        var builder = WebApplication.CreateBuilder(args);
        var key = Encoding.ASCII.GetBytes("MIC@TokenAutenticacaoWill@123MIC@TokenAutenticacaoWill@123");

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
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
            }) ;

            options.OperationFilter<SecurityRequirementsOperationFilter>(); 
        });
        builder.Services.AddScoped<EmailService>();
        builder.Services.AddScoped<PermissoesService>();
        builder.Services.AddScoped<TelefoneService>();
        builder.Services.AddScoped<ColaboradorService>();
        builder.Services.AddScoped<LoginService>();
        builder.Services.AddCors(options => { options.AddPolicy("MyAllowedOrigins", builder => { builder.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }); });

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = null;
        });

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

        app.UseCors("MyAllowedOrigins");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}