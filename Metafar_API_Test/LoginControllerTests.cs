using Moq;
using Metafar_API.Models.Custom;
using Metafar_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Metafar_API.Controllers;


namespace Metafar_API_Test
{
    public class LoginControllerTests
    {

        [Test]
        public async Task Autenticar_DeberiaRetornarUnauthorized_CuandoDevolverTokenEsNull()
        {
            var mockAutorizacionService = new Mock<IAutorizacionService>();
            mockAutorizacionService.Setup(service => service.DevolverToken(It.IsAny<AutorizacionRequest>()))
                .ReturnsAsync(null as AutorizacionResponse);

            var loginController = new LoginController(mockAutorizacionService.Object);

            var resultado = await loginController.Autenticar(new AutorizacionRequest());

            Assert.That(resultado, Is.TypeOf<UnauthorizedResult>());
        }


        [Test]
        public async Task Autenticar_DeberiaRetornarOk_CuandoDevolverTokenNoEsNull()
        {
            var autorizacionResponseMock = new AutorizacionResponse
            {
            };

            var mockAutorizacionService = new Mock<IAutorizacionService>();
            mockAutorizacionService
                .Setup(service => service.DevolverToken(It.IsAny<AutorizacionRequest>()))
                .ReturnsAsync(autorizacionResponseMock);

            var loginController = new LoginController(mockAutorizacionService.Object);

            var resultado = await loginController.Autenticar(new AutorizacionRequest());

            Assert.That(resultado, Is.TypeOf<OkObjectResult>());
        }
    }
}
