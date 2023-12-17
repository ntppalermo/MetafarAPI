using Microsoft.AspNetCore.Mvc;
using Metafar_API.Models.Custom;
using Metafar_API.Services.Interfaces;
using Serilog;
using Metafar_API.Helper;

namespace Metafar_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetiroController : ControllerBase
    {
        private readonly IExtraccionService _extraccionService;

        public RetiroController(IExtraccionService extraccionService)
        {
            _extraccionService = extraccionService;
        }

        [HttpPost("RealizarExtraccion")]
        public ActionResult<OperacionResponse> RealizarExtraccion([FromBody] ExtraccionRequest request)
        {
            try
            {
                if (!AuthorizationHelper.ValidateNumeroTarjeta(User, request.NumeroTarjeta))
                {
                    return BadRequest("El número de tarjeta en la solicitud no coincide con el número de tarjeta autorizado en el login.");
                }

                var operacionResponse = _extraccionService.RealizarExtraccion(request.NumeroTarjeta, request.Monto);

                if (operacionResponse.CodigoError != null)
                {
                    return BadRequest(operacionResponse.CodigoError);
                }

                return Ok(operacionResponse);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error interno al RealizarExtraccion");
                return new ErrorResult(400, "Solicitud de retiro invalida");
            }
        }
    }
}
