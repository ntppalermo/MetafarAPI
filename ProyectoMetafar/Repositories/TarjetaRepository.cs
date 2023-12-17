using Microsoft.EntityFrameworkCore;
using Metafar_API.Data;
using Metafar_API.Data.Entities;
using Metafar_API.Repositories.Interfaces;

namespace Metafar_API.Repositories
{
    public class TarjetaRepository : ITarjetaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TarjetaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Tarjeta ObtenerTarjetaPorNumero(int numeroTarjeta)
        {
            return _dbContext.Tarjetas
                    .Include(t => t.Usuario)
                    .FirstOrDefault(t => t.NumeroTarjeta == numeroTarjeta);
        }

        public void GuardarHistorialOperacion(Operacion operacion)
        {
            _dbContext.Operaciones.Add(operacion);
            _dbContext.SaveChanges();
        }

        public void ActualizarSaldo(Tarjeta tarjeta)
        {
            _dbContext.SaveChanges();
        }
    }
}
