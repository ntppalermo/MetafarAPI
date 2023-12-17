using Microsoft.EntityFrameworkCore;
using Metafar_API.Data;
using Metafar_API.Data.Entities;
using Metafar_API.Repositories.Interfaces;

namespace Metafar_API.Repositories
{
    public class OperacionRepository : IOperacionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OperacionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AgregarOperacion(Operacion operacion)
        {
            _dbContext.Operaciones.Add(operacion);
            _dbContext.SaveChanges();
        }

        public async Task<int> ObtenerTotalRegistrosAsync(int numeroTarjeta)
        {
            return await _dbContext.Operaciones
                             .Where(o => o.NumeroTarjeta == numeroTarjeta)
                             .CountAsync();

        }

        public async Task<List<Operacion>> ObtenerHistorialOperacionesAsync(int numeroTarjeta, int pagina, int tamanoPagina)
        {
             return await _dbContext.Operaciones
                .Where(o => o.NumeroTarjeta == numeroTarjeta)
                .OrderByDescending(o => o.FechaOperacion)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();
        }
    }
}
