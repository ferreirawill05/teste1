using Mac.MadeInCotia.Entities.Emails;
using MAC.MadeInCotia.Biz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mac.MadeInCotia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailsController(EmailService emailService) 
        {
            _emailService = emailService;
        }

        [Authorize]
        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            var email = _emailService.ConsultaPorId(id);
            if (email == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(email);
            }
        }

        [Authorize]
        [HttpGet("/EmailTodos")]
        public IActionResult GetAll() 
        {
            var emailUsuario = _emailService.ConsultaTodos();
            return Ok(emailUsuario);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateEmail(EmailsViewModel email)
        {
            EmailsViewModel emailUsuario = _emailService.CriarEmail(email);
            if (emailUsuario == null)
            {
                return BadRequest("Error");
            }

            return Ok(email);
        }

        [Authorize]
        [HttpDelete("/deletaUm")]
        public IActionResult Delete(EmailsViewModel email) {
            EmailsViewModel mensagem = _emailService.DeletarEmail(email);

            return Ok(mensagem);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put(EmailsViewModel email)
        {
            EmailsViewModel mensagem = _emailService.AtualizarEmail(email);

            return Ok(mensagem);
        }
      
    }
}