using Microsoft.AspNetCore.Mvc;
using Metafar_API.Models.Custom;
using Metafar_API.Services.Interfaces;
using Serilog;

namespace Metafar_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAutorizacionService _autorizacionService;

        public LoginController(IAutorizacionService autorizacionService)
        {
            _autorizacionService = autorizacionService;
        }

        [HttpPost]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] AutorizacionRequest autorizacion)
        {
            try
            {
                var resultado_autorizacion = await _autorizacionService.DevolverToken(autorizacion);
                if (resultado_autorizacion == null)
                    return Unauthorized();

                return Ok(resultado_autorizacion);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error interno al Autenticar");

                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
