using Microsoft.Extensions.Configuration;
using Metafar_API.Models.Custom;
using Metafar_API.Repositories.Interfaces;
using Metafar_API.Services;
using Moq;
using Metafar_API.Data.Entities;

namespace Metafar_API_Test
{
    [TestFixture]
    public class AuthServiceTests
    {
        [Test]
        public async Task DevolverTokenDeberiaRetornarUnauthorizedCuandoTarjetaNoEncontrada()
        {
            var mockAutorizacionRepository = new Mock<IAutorizacionRepository>();
            mockAutorizacionRepository
                .Setup(repo => repo.ObtenerUsuarioPorTarjetaAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Usuario);

            var mockConfiguration = new Mock<IConfiguration>();

            var autorizacionService = new AutorizacionService(
                mockAutorizacionRepository.Object,
                mockConfiguration.Object);

            var resultado = await autorizacionService.DevolverToken(new AutorizacionRequest { NumeroTarjeta = 123456 });

            Assert.That(resultado, Is.InstanceOf<AutorizacionResponse>());
            Assert.Multiple(() =>
            {
                Assert.That(resultado.Resultado, Is.False);
                Assert.That(resultado.Msg, Is.EqualTo("Tarjeta no encontrada o bloqueada"));
            });
        }
    }
}
