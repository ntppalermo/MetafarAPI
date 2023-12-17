using Metafar_API.Models.Custom;

namespace Metafar_API.Services.Interfaces
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);
    }
}
