using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Colaborador;
using MAC.MadeInCotia.Biz.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Mac.MadeInCotia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {
        private readonly ColaboradorService _colaboradorService;

        public ColaboradoresController(ColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }

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

        [HttpPost("/criar")]
        public IActionResult CreateUser(ColaboradorViewModel colaborador)
        {
            ColaboradorViewModel usuario = _colaboradorService.CriarUsuario(colaborador);
            if (usuario == null)
            {
                return BadRequest("Error");
            }

            return Ok(colaborador);
        }

        [HttpDelete("/deletar")]
        public IActionResult DeleteUser(ColaboradorViewModel colaborador)
        {
            ColaboradorViewModel usuario = _colaboradorService.DeletarUsuario(colaborador);

            return Ok(usuario);
        }

        [HttpPut("/atualizar")]
        public IActionResult UpdateUser(ColaboradorViewModel colaborador)
        {
            ColaboradorViewModel usuario = _colaboradorService.AtualizarUsuario(colaborador);
            return Ok(usuario);
        }

        [HttpPut("/atualizarSenha")]
        public IActionResult UpdatePassword(ColaboradorAlterarSenhaViewModel senha)
        {
            ColaboradorAlterarSenhaViewModel senhaUsuario = _colaboradorService.AtualizarSenha(senha);
            return Ok(senhaUsuario);
        }

        //Gerador de Token

        /*[HttpGet]
        public IActionResult GetToken(CF_Colaborador colaborador) 
        {
            ColaboradorViewModel colaboradorToken = _colaboradorService.GerarToken(colaborador);
            return Ok(colaboradorToken);
        }*/

        //----------------

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



    }
}