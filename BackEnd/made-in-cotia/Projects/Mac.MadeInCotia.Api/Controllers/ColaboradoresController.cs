using Mac.MadeInCotia.Entities.Colaborador;
using MAC.MadeInCotia.Biz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        /*public IActionResult GetMe()
        {
            int id = int.Parse(HttpContext.User.FindFirstValue("IdTipoUsuario"));
            return CustomReturn(_colaboradorService.GetMe(id));
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
