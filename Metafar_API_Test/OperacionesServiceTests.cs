using Metafar_API.Data.Entities;
using Metafar_API.Repositories.Interfaces;
using Metafar_API.Services;
using Moq;

namespace Metafar_API_Test
{
    [TestFixture]
    public class OperacionesServiceTests
    {

        [Test]
        public async Task ObtenerHistorialDeberiaRetornarDatosCorrectos()
        {
            var mockOperacionRepository = new Mock<IOperacionRepository>();
            mockOperacionRepository
                .Setup(repo => repo.ObtenerTotalRegistrosAsync(It.IsAny<int>()))
                .ReturnsAsync(5);
            mockOperacionRepository
                .Setup(repo => repo.ObtenerHistorialOperacionesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Operacion>
                {
                    new Operacion { },
                    new Operacion { },
                });

            var operacionService = new OperacionService(mockOperacionRepository.Object);

            var (TotalRegistros, TotalPaginas, Historial) = await operacionService.ObtenerHistorialOperacionesAsync(123456789, 1, 10);

            Assert.Multiple(() =>
            {
                Assert.That(TotalRegistros, Is.EqualTo(5));
                Assert.That(TotalPaginas, Is.EqualTo(1));
                Assert.That(Historial, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public async Task ObtenerHistorialDeberiaRetornarCeroCuandoTarjetaNoEncontrada()
        {
            var mockOperacionRepository = new Mock<IOperacionRepository>();
            mockOperacionRepository
                .Setup(repo => repo.ObtenerTotalRegistrosAsync(It.IsAny<int>()))
                .ReturnsAsync(0);

            var operacionService = new OperacionService(mockOperacionRepository.Object);

            var (TotalRegistros, TotalPaginas, Historial) = await operacionService.ObtenerHistorialOperacionesAsync(123456789, 1, 10);

            Assert.Multiple(() =>
            {
                Assert.That(TotalRegistros, Is.EqualTo(0));
                Assert.That(TotalPaginas, Is.EqualTo(0));
            });

            Assert.That(Historial, Is.Null);
        }
    }
}
