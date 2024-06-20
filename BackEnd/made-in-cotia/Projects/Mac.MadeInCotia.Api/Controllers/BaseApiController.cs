using Mac.MadeInCotia.Data.Context;
using MAC.MadeInCotia.Biz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mac.MadeInCotia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {   

        public string Responsavel
        {
            get
            {
                HttpContext.Request.Headers.TryGetValue("MIC@TokenAutenticacaoWill@123MIC@TokenAutenticacaoWill@123", out StringValues requestProperty);

                if (!string.IsNullOrWhiteSpace(requestProperty[0]))
                    return requestProperty.ToString();
                else
                    return "sistema";
            }
        }

       /* [HttpGet("/GetMe")]
        [Authorize] // Garante que somente requisições autenticadas possam acessar este método
        public IActionResult GetMe()
        {
            // Recuperando o ID do colaborador do Claim no token JWT
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var colaboradorId = userClaims.FirstOrDefault(c => c.Type == "IdColaborador")?.Value;

                if (!string.IsNullOrEmpty(colaboradorId))
                {
                    var colaborador = _context.CF_Colaborador.Find(int.Parse(colaboradorId));
                    if (colaborador != null)
                    {
                        return Ok(colaborador); // Retorna os dados do colaborador
                    }
                    return NotFound("Colaborador não encontrado.");
                }
            }
            return BadRequest("ID do colaborador não encontrado no token.");
        }*/
    }
}
