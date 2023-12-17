using Metafar_API.Data.Entities;
using Metafar_API.Data;
using Metafar_API.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Metafar_API_Test
{
    [TestFixture]
    public class OperacionRepositoryTests
    {
        [Test]
        public void AgregarOperacion_DeberiaAgregarOperacionCorrectamente()
        {
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockOperacionesDbSet = new Mock<DbSet<Operacion>>();
            var operacionRepository = new OperacionRepository(mockDbContext.Object);

            var operacion = new Operacion {
                OperacionID = 1,
                NumeroTarjeta = 123456789,
                TipoOperacionID = 2,
                MontoExtraido = 50.0m,
                FechaOperacion = DateTime.Now,
                TipoOperacion = new TipoOperacion { TipoOperacionID = 2, Nombre = "Extraccion" }
            };

            mockDbContext.Setup(c => c.Operaciones).Returns(mockOperacionesDbSet.Object);

            operacionRepository.AgregarOperacion(operacion);

            mockOperacionesDbSet.Verify(set => set.Add(It.IsAny<Operacion>()), Times.Once);
            mockDbContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
