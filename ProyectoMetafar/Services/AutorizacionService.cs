using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Metafar_API.Models.Custom;
using Metafar_API.Services.Interfaces;
using Metafar_API.Repositories.Interfaces;

namespace Metafar_API.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly IAutorizacionRepository _autorizacionRepository;
        private readonly IConfiguration _configuration;

        public AutorizacionService(
            IAutorizacionRepository autorizacionRepository,
            IConfiguration configuration)
        {
            _autorizacionRepository = autorizacionRepository;
            _configuration = configuration;
        }

        private string GenerarToken(string idUsuario, string numeroTarjeta)
        {
            var key = _configuration["TokenSettings:TokenKey"];
            var keyBytes = Encoding.ASCII.GetBytes(key);


            var claims = new ClaimsIdentity(new[]
            {
                new Claim("Uid", idUsuario),
                new Claim("Nta", numeroTarjeta),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64)
            });


            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenConfig);
        }
        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            var usuario = await _autorizacionRepository.ObtenerUsuarioPorTarjetaAsync(autorizacion.NumeroTarjeta);


            var tarjetaEncontrada = usuario?.Tarjetas.SingleOrDefault(x => x.NumeroTarjeta == autorizacion.NumeroTarjeta);

            if (tarjetaEncontrada == null || tarjetaEncontrada.Bloqueada)
            {
                return new AutorizacionResponse { Resultado = false, Msg = "Tarjeta no encontrada o bloqueada" };
            }

            if (tarjetaEncontrada.Pin != autorizacion.Pin)
            {
                tarjetaEncontrada.IntentosFallidos++;

                if (tarjetaEncontrada.IntentosFallidos >= 5)
                {
                    tarjetaEncontrada.Bloqueada = true;

                    await _autorizacionRepository.GuardarCambiosAsync();

                    return new AutorizacionResponse { Resultado = false, Msg = "Tarjeta bloqueada debido a múltiples intentos fallidos" };

                }

                await _autorizacionRepository.GuardarCambiosAsync();

                return new AutorizacionResponse { Resultado = false, Msg = "PIN incorrecto" };
            }

            tarjetaEncontrada.IntentosFallidos = 0;
            await _autorizacionRepository.GuardarCambiosAsync();

            string tokenCreado = GenerarToken(tarjetaEncontrada.UsuarioID.ToString(), autorizacion.NumeroTarjeta.ToString());

            return new AutorizacionResponse { Token = tokenCreado, Resultado = true, Msg = "Ok" };
        }
    }
}
