using Metafar_API.Data.Entities;
using Metafar_API.Repositories.Interfaces;
using Metafar_API.Services;
using Moq;

namespace Metafar_API_Test
{
    [TestFixture]
    public class TarjetaServiceTests
    {
        [Test]
        public void ObtenerTarjetaPorNumeroDeberiaRetornarSaldoResponseCorrecto()
        {
            var mockTarjetaRepository = new Mock<ITarjetaRepository>();
            var tarjetaService = new TarjetaService(mockTarjetaRepository.Object);

            var numeroTarjeta = 123456789;
            var usuarioNombre = "NombreDeUsuario";
            var usuarioNumeroCuenta = 12345;
            var tarjetaSaldo = 100.00m;
            var tarjetaFechaUltimaExtraccion = DateTime.Now;

            mockTarjetaRepository.Setup(repo => repo.ObtenerTarjetaPorNumero(It.IsAny<int>()))
                .Returns(new Tarjeta
                {
                    Usuario = new Usuario
                    {
                        Nombre = usuarioNombre,
                        NumeroCuenta = usuarioNumeroCuenta
                    },
                    Saldo = tarjetaSaldo,
                    FechaUltimaExtraccion = tarjetaFechaUltimaExtraccion
                });

            var resultado = tarjetaService.ObtenerSaldo(numeroTarjeta);

            Assert.That(resultado, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(resultado.NombreUsuario, Is.EqualTo(usuarioNombre));
                Assert.That(resultado.NumeroCuenta, Is.EqualTo(usuarioNumeroCuenta));
                Assert.That(resultado.SaldoActual, Is.EqualTo(tarjetaSaldo));
                Assert.That(resultado.FechaUltimaExtraccion, Is.EqualTo(tarjetaFechaUltimaExtraccion));
            });
        }
    }
}
