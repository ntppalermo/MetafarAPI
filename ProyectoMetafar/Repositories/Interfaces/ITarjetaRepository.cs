using Metafar_API.Data.Entities;

namespace Metafar_API.Repositories.Interfaces
{
    public interface ITarjetaRepository
    {
        Tarjeta ObtenerTarjetaPorNumero(int numeroTarjeta);
        void ActualizarSaldo(Tarjeta tarjeta);
        void GuardarHistorialOperacion(Operacion operacion);
    }
}
