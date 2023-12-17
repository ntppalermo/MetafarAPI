using Microsoft.AspNetCore.Mvc;
using Metafar_API.Models.Custom;
using Metafar_API.Services.Interfaces;
using Serilog;
using Metafar_API.Attributes;

namespace Metafar_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SaldoController : ControllerBase
    {
        private readonly ITarjetaService _tarjetaService;

        public SaldoController(ITarjetaService tarjetaService)
        {
            _tarjetaService = tarjetaService;
        }

        [CustomAuthorize]
        [HttpGet("ObtenerSaldo")]
        public ActionResult<SaldoResponse> ObtenerSaldo([FromQuery] int numeroTarjeta)
        {
            try
            {
                var saldoResponse = _tarjetaService.ObtenerSaldo(numeroTarjeta);

                if (saldoResponse == null)
                {
                    return NotFound("Tarjeta no encontrada");
                }

                return Ok(saldoResponse);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error interno al ObtenerSaldo");

                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
