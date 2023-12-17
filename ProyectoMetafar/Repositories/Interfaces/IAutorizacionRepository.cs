using Metafar_API.Data.Entities;

namespace Metafar_API.Repositories.Interfaces
{
    public interface IAutorizacionRepository
    {
        Task<Usuario> ObtenerUsuarioPorTarjetaAsync(int numeroTarjeta);
        Task GuardarCambiosAsync();
    }
}
