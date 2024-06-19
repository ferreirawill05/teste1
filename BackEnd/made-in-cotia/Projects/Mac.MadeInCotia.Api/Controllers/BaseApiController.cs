using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

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
                HttpContext.Request.Headers.TryGetValue("TipoUsuario", out StringValues requestProperty);

                if (!string.IsNullOrWhiteSpace(requestProperty[0]))
                    return requestProperty.ToString();
                else
                    return "sistema";
            }
        }


    }
}
