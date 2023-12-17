using Metafar_API.Data.Entities;
using Metafar_API.Repositories.Interfaces;
using Metafar_API.Services;
using Moq;

namespace Metafar_API_Test
{
    [TestFixture]
    public class ExtraccionServiceTests
    {
        [Test]
        public void RealizarExtraccionDeberiaRetornarMensajeTarjetaNoEncontrada()
        {
            var numeroTarjeta = 123456789;
            var monto = 100;
            var mockTarjetaRepository = new Mock<ITarjetaRepository>();
            mockTarjetaRepository
                .Setup(repo => repo.ObtenerTarjetaPorNumero(It.IsAny<int>()))
                .Returns((Tarjeta)null!);

            var extraccionService = new ExtraccionService(mockTarjetaRepository.Object);

            var resultado = extraccionService.RealizarExtraccion(numeroTarjeta, monto);
            Assert.Multiple(() =>
            {
                Assert.That(resultado.CodigoError, Is.EqualTo("Tarjeta no encontrada"));
                Assert.That(resultado.OperacionExitosa, Is.False);
            });
        }

        [Test]
        public void RealizarExtraccionDeberiaRetornarMensajeSaldoInsuficiente()
        {
            var numeroTarjeta = 987654321;
            var monto = 200;

            var mockTarjetaRepository = new Mock<ITarjetaRepository>();
            mockTarjetaRepository
             .Setup(repo => repo.ObtenerTarjetaPorNumero(It.IsAny<int>()))
             .Returns((int numero) => new Tarjeta { NumeroTarjeta = numero, Saldo = 150 });

            var extraccionService = new ExtraccionService(mockTarjetaRepository.Object);

            var resultado = extraccionService.RealizarExtraccion(numeroTarjeta, monto);

            Assert.That(resultado.CodigoError, Is.EqualTo("Saldo insuficiente."));
            Assert.That(resultado.OperacionExitosa, Is.False);
        }

        [Test]
        public void RealizarExtraccionDeberiaRetornarOperacionExitosa()
        {
            var numeroTarjeta = 456789012;
            var monto = 50;
            var mockTarjetaRepository = new Mock<ITarjetaRepository>();
            mockTarjetaRepository
           .Setup(repo => repo.ObtenerTarjetaPorNumero(It.IsAny<int>()))
           .Returns((int numero) => new Tarjeta { NumeroTarjeta = numero, Saldo = 150 });

            var mockOperacionRepository = new Mock<IOperacionRepository>();
            var extraccionService = new ExtraccionService(mockTarjetaRepository.Object);

            var resultado = extraccionService.RealizarExtraccion(numeroTarjeta, monto);
            Assert.Multiple(() =>
            {
                Assert.That(resultado.CodigoError, Is.Null.Or.Empty);
                Assert.That(resultado.OperacionExitosa, Is.True);
            });
        }
    }
}
