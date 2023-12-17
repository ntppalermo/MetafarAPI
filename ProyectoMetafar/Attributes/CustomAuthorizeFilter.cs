using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Metafar_API.Attributes
{
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private const string NumeroTarjetaClaimType = "Nta";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!IsGetRequest(context.HttpContext.Request))
            {
                return;
            }

            if (!IsUserAuthenticated(context.HttpContext.User))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!IsValidNumeroTarjetaClaim(context.HttpContext.User.FindFirst(NumeroTarjetaClaimType)))
            {
                context.Result = UnauthorizedResult("Número de tarjeta no válido");
                return;
            }

            var numeroTarjetaValue = context.HttpContext.Request.Query["numeroTarjeta"];
            if (!IsValidNumeroTarjetaValue(numeroTarjetaValue))
            {
                context.Result = UnauthorizedResult("Número de tarjeta no válido en la solicitud");
                return;
            }

            if (!IsAuthorized(numeroTarjetaValue, context.HttpContext.User))
            {
                context.Result = UnauthorizedResult("El número de tarjeta en la solicitud no coincide con el número de tarjeta autorizado en el login.");
                return;
            }
        }

        private static bool IsGetRequest(HttpRequest request) => request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase);

        private static bool IsUserAuthenticated(ClaimsPrincipal user) => user.Identity.IsAuthenticated;

        private static bool IsValidNumeroTarjetaClaim(Claim numeroTarjetaClaim) =>
            numeroTarjetaClaim != null && int.TryParse(numeroTarjetaClaim.Value, out _);

        private static bool IsValidNumeroTarjetaValue(string numeroTarjetaValue) =>
            !string.IsNullOrEmpty(numeroTarjetaValue) && int.TryParse(numeroTarjetaValue, out _);

        private static bool IsAuthorized(string numeroTarjetaValue, ClaimsPrincipal user) =>
            numeroTarjetaValue == user.FindFirst(NumeroTarjetaClaimType)?.Value;

        private static UnauthorizedObjectResult UnauthorizedResult(string message) =>
            new(message);
    }
}
