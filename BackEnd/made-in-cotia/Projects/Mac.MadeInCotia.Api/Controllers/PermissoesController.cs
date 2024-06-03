using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Permissoes;
using MAC.MadeInCotia.Biz.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mac.MadeInCotia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissoesController : ControllerBase 
    {
        private readonly PermissoesService _permissaoService;

        public PermissoesController(PermissoesService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        [HttpGet]
        public IActionResult GetPermission(int id) 
        { 
            var permissao = _permissaoService.ConsultaPorId(id);
            if (permissao == null)
            {
                return NotFound();
            }
            return Ok(permissao);
        }

        [HttpGet("/permissaoTodos")]
        public IActionResult GetAll()
        {
            var permissaoTotal = _permissaoService.ConsultaTodos();
            if (permissaoTotal == null)
            {
                return BadRequest();
            }
            return Ok(permissaoTotal);
        }

        [HttpPost]
        public IActionResult CreatePermission(PermissoesViewModel permissao)
        {
            PermissoesViewModel permissaoColaborador = _permissaoService.CriarPermissao(permissao);
            if (permissaoColaborador == null)
            {
                return BadRequest();
            }
            return Ok(permissao);
        }

        [HttpDelete]
        public IActionResult DeletePermission(PermissoesViewModel permissao) 
        {
            PermissoesViewModel permissaoColaborador = _permissaoService.DeletarPermissao(permissao);
            if (permissaoColaborador == null)
            {
                return BadRequest();
            }
            return Ok(permissao);
        }

        [HttpPut]
        public IActionResult UpdatePermission(PermissoesViewModel permissao)
        {
            PermissoesViewModel permissaoColaborador = _permissaoService.AtualizarPermissao(permissao);
            return Ok(permissaoColaborador);
        }
    }
}
