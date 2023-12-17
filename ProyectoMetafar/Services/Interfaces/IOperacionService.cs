using Metafar_API.Data.Entities;

namespace Metafar_API.Services.Interfaces
{
    public interface IOperacionService
    {
         Task<(int TotalRegistros, int TotalPaginas, List<Operacion> Historial)> ObtenerHistorialOperacionesAsync(int numeroTarjeta, int pagina, int registrosPorPagina);
    }
}
