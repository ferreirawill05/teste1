using Mac.MadeInCotia.Data.Context;
using Mac.MadeInCotia.Entities.Colaborador;
using MAC.MadeInCotia.Biz.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mac.MadeInCotia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        private readonly MacMadeInCotiaContext _context;

        public LoginController(LoginService loginService, MacMadeInCotiaContext context)
        {
            _loginService = loginService;
            _context = context;
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var usuarioLogin = _loginService.Logar(loginViewModel);

            return Ok(usuarioLogin);
        }


    }



}

 