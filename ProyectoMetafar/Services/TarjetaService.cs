using Metafar_API.Models.Custom;
using Metafar_API.Services.Interfaces;
using Metafar_API.Data.Entities;
using Metafar_API.Repositories.Interfaces;

namespace Metafar_API.Services
{
    public class TarjetaService : ITarjetaService
    {
        private readonly ITarjetaRepository _tarjetaRepository;

        public TarjetaService(ITarjetaRepository tarjetaRepository)
        {
            _tarjetaRepository = tarjetaRepository;
        }

        public SaldoResponse ObtenerSaldo(int numeroTarjeta)
        {
            var tarjeta = _tarjetaRepository.ObtenerTarjetaPorNumero(numeroTarjeta);

            if (tarjeta != null)
            {
                GuardarHistorialOperacionesSaldo(tarjeta);

                return new SaldoResponse
                {
                    NombreUsuario = tarjeta.Usuario.Nombre,
                    NumeroCuenta = tarjeta.Usuario.NumeroCuenta,
                    SaldoActual = tarjeta.Saldo,
                    FechaUltimaExtraccion = tarjeta.FechaUltimaExtraccion
                };
            }

            return null;
        }

        private void GuardarHistorialOperacionesSaldo(Tarjeta tarjeta)
        {
            _tarjetaRepository.GuardarHistorialOperacion(new Operacion
            {
                FechaOperacion = DateTime.Now,
                TipoOperacionID = 1,
                NumeroTarjeta = tarjeta.NumeroTarjeta,
                MontoExtraido = null
            });
        }
    }
}
