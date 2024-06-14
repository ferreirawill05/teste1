using Mac.MadeInCotia.Entities.Telefones;
using MAC.MadeInCotia.Biz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mac.MadeInCotia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonesController : ControllerBase
    {
        private readonly TelefoneService _telefoneService;

        public TelefonesController(TelefoneService telefoneService)
        {
            _telefoneService = telefoneService;
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var colaboradorTelefone = _telefoneService.ConsultaPorId(id);
            {
                if (colaboradorTelefone == null)
                { 
                    return NotFound();
                }

                return Ok(colaboradorTelefone);
            }
        }


        [HttpGet("TelefoneTodos")]
        public IActionResult GetAllPhones()
        {
            var colaboradorTelefone = _telefoneService.ConsultaTodos();
            {
                if (colaboradorTelefone == null)
                {
                    return NotFound();
                }

                return Ok(colaboradorTelefone);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePhone (TelefonesViewModel telefone)
        {
            TelefonesViewModel colaboradorTelefone = _telefoneService.CriarTelefone(telefone);
            if (colaboradorTelefone == null)
            {
                return BadRequest();
            }
            return Ok(telefone);
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeletePhone(TelefonesViewModel telefone) 
        {
            TelefonesViewModel colaboradorTelefone = _telefoneService.DeletarTelefone(telefone);
            if (colaboradorTelefone == null)
            {
                return BadRequest();
            }
            return Ok(telefone);
        }

        [HttpPut]
        public IActionResult UpdatePhone(TelefonesViewModel telefone)
        {
            TelefonesViewModel colaboradorTelefone = _telefoneService.AtuaizarTelefone(telefone);
            if (colaboradorTelefone == null)
            {
                return BadRequest();
            }
            return Ok(telefone);
        }

        [Authorize]
        [HttpPut("/flag")]
        public IActionResult UpdateFla(TelefonesViewModel flag)
        {
            TelefonesViewModel telefoneFlag = _telefoneService.AtualizarFlag(flag);
            if (telefoneFlag == null)
            {
                return BadRequest();
            }
            return Ok(flag);
        }
    }

}  
