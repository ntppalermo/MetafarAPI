using Metafar_API.Models.Custom;
using Metafar_API.Services.Interfaces;
using Metafar_API.Data.Entities;
using Metafar_API.Repositories.Interfaces;

namespace Metafar_API.Services
{
    public class ExtraccionService : IExtraccionService
    {
        private readonly ITarjetaRepository _tarjetaRepository;

        public ExtraccionService(ITarjetaRepository tarjetaRepository)
        {
            _tarjetaRepository = tarjetaRepository;
        }

        public OperacionResponse RealizarExtraccion(int numeroTarjeta, decimal monto)
        {
            var tarjeta = _tarjetaRepository.ObtenerTarjetaPorNumero(numeroTarjeta);

            if (tarjeta == null)
            {
                return new OperacionResponse { 
                    CodigoError = "Tarjeta no encontrada", 
                    OperacionExitosa = false 
                };
            }

            if (tarjeta.Saldo < monto)
            {
                return new OperacionResponse {
                    CodigoError = $"Saldo insuficiente.",
                    OperacionExitosa = false
                };
            }

            tarjeta.Saldo -= monto;
            _tarjetaRepository.ActualizarSaldo(tarjeta);

            _tarjetaRepository.GuardarHistorialOperacion(new Operacion
            {
                FechaOperacion = DateTime.Now,
                TipoOperacionID = 2,
                NumeroTarjeta = tarjeta.NumeroTarjeta,
                MontoExtraido = monto
            });

            return new OperacionResponse
            {
                MontoRetirado = monto,
                NuevoSaldo = tarjeta.Saldo,
                Resumen = $"Extracción exitosa de {monto:C}. Nuevo saldo: {tarjeta.Saldo:C}",
                OperacionExitosa = true
            };
        }
    }
}
