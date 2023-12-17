using Microsoft.EntityFrameworkCore;
using Metafar_API.Data;
using Metafar_API.Data.Entities;
using Metafar_API.Repositories.Interfaces;

namespace Metafar_API.Repositories
{
    public class AutorizacionRepository : IAutorizacionRepository
    {
        private readonly IApplicationDbContext _context;

        public AutorizacionRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObtenerUsuarioPorTarjetaAsync(int numeroTarjeta)
        {
            return await _context.Usuarios
                .Include(u => u.Tarjetas)
                .Where(u => u.Tarjetas.Any(t => t.NumeroTarjeta == numeroTarjeta))
                .FirstOrDefaultAsync();
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
