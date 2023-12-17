using System.Security.Claims;

namespace Metafar_API.Helper
{
    public static class AuthorizationHelper
    {
        public static bool ValidateNumeroTarjeta(ClaimsPrincipal user, int numeroTarjeta)
        {
            var numeroTarjetaClaim = user.FindFirst("Nta");

            if (numeroTarjetaClaim != null && int.TryParse(numeroTarjetaClaim.Value, out int numeroTarjetaClaimValue))
            {
                return numeroTarjeta == numeroTarjetaClaimValue;
            }

            return false;
        }
    }
}
