using Microsoft.EntityFrameworkCore;
using Metafar_API.Models.Custom;
using Metafar_API.Services.Interfaces;
using System.Net;
using Metafar_API.Data.Entities;
using Metafar_API.Repositories.Interfaces;

namespace Metafar_API.Services
{
    public class OperacionService : IOperacionService
    {
        private readonly IOperacionRepository _operacionRepository;

        public OperacionService(IOperacionRepository operacionRepository)
        {
            _operacionRepository = operacionRepository;
        }

        public async Task<(int TotalRegistros, int TotalPaginas, List<Operacion> Historial)> ObtenerHistorialOperacionesAsync(int numeroTarjeta, int pagina, int registrosPorPagina)
        {
            var totalRegistros = await _operacionRepository.ObtenerTotalRegistrosAsync(numeroTarjeta);

            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);

            var historial = await _operacionRepository.ObtenerHistorialOperacionesAsync(numeroTarjeta, pagina, registrosPorPagina);

            return (totalRegistros, totalPaginas, historial);
        }
    }
}
