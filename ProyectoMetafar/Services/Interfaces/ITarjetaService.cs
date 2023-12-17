using Metafar_API.Models.Custom;

namespace Metafar_API.Services.Interfaces
{
    public interface ITarjetaService
    {
        SaldoResponse ObtenerSaldo(int numeroTarjeta);
    }
}
