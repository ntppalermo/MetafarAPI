using Metafar_API.Data.Entities;
using Metafar_API.Data;
using Metafar_API.Repositories;
using Moq;
using MockQueryable.Moq;
using Metafar_API.Repositories.Interfaces;

namespace Metafar_API_Test
{
    [TestFixture]
    public class AutorizacionRepositoryTests
    {
        private Mock<IApplicationDbContext> _contextMock;
        private AutorizacionRepository _autorizacionRepository;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _autorizacionRepository = new AutorizacionRepository(_contextMock.Object);
        }

        [Test]
        public void ObtenerUsuarioPorTarjetaDeberiaRetornarUsuario()
        {
            var numeroTarjeta = 123456;
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    UsuarioID = 1,
                    Nombre = "Usuario1",
                    NumeroCuenta = 1234,
                    Tarjetas = new List<Tarjeta>
                    {
                        new Tarjeta { NumeroTarjeta = numeroTarjeta }
                    }
                },
            };

            var mockUsuarios = usuarios.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Usuarios).Returns(mockUsuarios.Object);

            var _autorizacionRepositoryMock = new Mock<IAutorizacionRepository>();
            _autorizacionRepositoryMock
                .Setup(repo => repo.ObtenerUsuarioPorTarjetaAsync(It.IsAny<int>()))
                .ReturnsAsync((int numTarjeta) => usuarios.SingleOrDefault(u => u.Tarjetas.Any(t => t.NumeroTarjeta == numTarjeta)));

            var resultado = _autorizacionRepositoryMock.Object.ObtenerUsuarioPorTarjetaAsync(numeroTarjeta).Result;

            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado.Nombre, Is.EqualTo("Usuario1"));
        }

        [Test]
        public async Task GuardarCambiosAsyncDeberiaGuardarCambiosEnContexto()
        {
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            await _autorizacionRepository.GuardarCambiosAsync();

            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
