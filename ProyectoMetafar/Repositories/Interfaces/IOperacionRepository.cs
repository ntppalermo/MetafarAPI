using Metafar_API.Data.Entities;

namespace Metafar_API.Repositories.Interfaces
{
    public interface IOperacionRepository
    {
        void AgregarOperacion(Operacion operacion);
        Task<int> ObtenerTotalRegistrosAsync(int numeroTarjeta);
        Task<List<Operacion>> ObtenerHistorialOperacionesAsync(int numeroTarjeta, int pagina, int registrosPorPagina);
    }
}
