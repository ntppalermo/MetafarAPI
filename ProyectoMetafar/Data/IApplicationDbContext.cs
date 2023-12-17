using Metafar_API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Metafar_API.Data
{
    public interface IApplicationDbContext : IDisposable
    {
         DbSet<Tarjeta> Tarjetas { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Operacion> Operaciones { get; set; }
        DbSet<TipoOperacion> TiposOperaciones { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}