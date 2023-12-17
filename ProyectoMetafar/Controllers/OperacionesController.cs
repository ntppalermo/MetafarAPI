using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Metafar_API.Entities;
using Metafar_API.Services.Interfaces;
using Metafar_API.Data.Entities;
using Serilog;
using Metafar_API.Attributes;

namespace Metafar_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperacionesController : ControllerBase
    {
        private readonly IOperacionService _operacionService;
        private readonly ConfiguracionPaginacion _configuracionPaginacion;

        public OperacionesController(IOperacionService operacionService, IOptions<ConfiguracionPaginacion> configuracionPaginacion)
        {
            _operacionService = operacionService;
            _configuracionPaginacion = configuracionPaginacion.Value;
        }

        [CustomAuthorize]
        [HttpGet("HistorialOperaciones")]
        public async Task<IActionResult> ObtenerHistorial([FromQuery] int numeroTarjeta, int pagina = 1, int registrosPorPagina = 10)
        {
            try
            {

                pagina = pagina == 0 ? _configuracionPaginacion.PaginaPorDefecto : pagina;
                registrosPorPagina = registrosPorPagina == 0 ? _configuracionPaginacion.RegistrosPorPaginaPorDefecto : registrosPorPagina;

                (int totalRegistros, int totalPaginas, List<Operacion> historial) = await _operacionService.ObtenerHistorialOperacionesAsync(numeroTarjeta, pagina, registrosPorPagina);

                var respuesta = new
                {
                    TotalRegistros = totalRegistros,
                    TotalPaginas = totalPaginas,
                    PaginaActual = pagina,
                    RegistrosPorPagina = registrosPorPagina,
                    Historial = historial
                };

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el historial de operaciones para la tarjeta {NumeroTarjeta}.", numeroTarjeta);

                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
