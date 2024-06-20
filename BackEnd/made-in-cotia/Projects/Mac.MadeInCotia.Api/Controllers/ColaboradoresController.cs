using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Colaborador;
using MAC.MadeInCotia.Biz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mac.MadeInCotia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradoresController : BaseApiController
    {
        private readonly ColaboradorService _colaboradorService;

        public ColaboradoresController(ColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }

        /*[HttpGet("GetMe")]
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
                    var colaborador = _context.Find(int.Parse(colaboradorId));
                    if (colaborador != null)
                    {
                        return Ok(colaborador); // Retorna os dados do colaborador
                    }
                    return NotFound("Colaborador não encontrado.");
                }
            }
            return BadRequest("ID do colaborador não encontrado no token.");
        }*/

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var colaborador = _colaboradorService.ConsultaPorId(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            return Ok(colaborador);
        }

        [Authorize]
        [HttpGet("/todos")]
        public IActionResult GetAllUsers()
        {
            var colaborador = _colaboradorService.ConsultaTodos();
            if (colaborador == null)
            {
                return NotFound();
            }

            return Ok(colaborador);
        }

        [Authorize]
        [HttpPost("/criar")]
        public IActionResult CreateUser(ColaboradorViewModel colaborador)
        {
            int usuario = _colaboradorService.CriarUsuario(colaborador);
            if (usuario == 0)
            {
                return BadRequest("Error");
            }

            return Ok(new {idColaborador = usuario});
        }

        [Authorize]
        [HttpDelete("/deletar")]
        public IActionResult DeleteUser(ColaboradorViewModel colaborador)
        {
            ColaboradorViewModel usuario = _colaboradorService.DeletarUsuario(colaborador);

            return Ok(usuario);
        }

        [Authorize]
        [HttpPut("/atualizar")]
        public IActionResult UpdateUser(ColaboradorViewModel colaborador)
        {
            ColaboradorViewModel usuario = _colaboradorService.AtualizarUsuario(colaborador);
            return Ok(usuario);
        }

        [HttpPut("/atualizarSenha")]
        public IActionResult UpdatePassword(ColaboradorAlterarSenhaViewModel colaborador)
        {
            ColaboradorAlterarSenhaViewModel senhaUsuario = _colaboradorService.AtualizarSenha(colaborador);
            return Ok(senhaUsuario);
        }

        /* ----- filtros -----*/

        /*Filtro mais recentes*/

        [HttpGet("/filtroRecente")]
        public IActionResult GetLatestFilter()
        {
            var usuario = _colaboradorService.BuscarRecente();
            return Ok(usuario);
        }

        /*Filtro mais antigos*/

        [HttpGet("/filtroAntigo")]
        public IActionResult GetOldesttFilter()
        {
            var usuario = _colaboradorService.BuscarAntigo();
            return Ok(usuario);
        }

        /*Filtro por período*/

        [HttpGet("/filtroPeriodo")]
        public IActionResult GetTimePeriod(DateTime firstDate, DateTime lastDate)
        {
            var usuario = _colaboradorService.BuscarPorPeriodo(firstDate, lastDate);
            return Ok(usuario);
        }

        /*Filtro Geral*/

        [HttpPost("/filtroGeral")]
        public IActionResult FilterAll(ColaboradorFiltroGeral colaborador)
        {

            var usuarios = _colaboradorService.SearchUsers(colaborador);

            if (usuarios == null || !usuarios.Colaboradores.Any())
            {
                return NotFound();
            }

            return Ok(usuarios);
        }
    }
}
