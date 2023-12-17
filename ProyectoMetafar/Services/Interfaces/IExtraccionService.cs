using Metafar_API.Models.Custom;

namespace Metafar_API.Services.Interfaces
{
    public interface IExtraccionService
    {
        OperacionResponse RealizarExtraccion(int numeroTarjeta, decimal monto);
    }
}
