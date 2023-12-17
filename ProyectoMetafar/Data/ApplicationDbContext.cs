using Metafar_API.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Metafar_API.Data
{
    public partial class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tarjeta> Tarjetas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Operacion> Operaciones { get; set; }
        public virtual DbSet<TipoOperacion> TiposOperaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Operacion>()
                .Property(o => o.MontoExtraido)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Tarjeta>()
                .Property(t => t.Saldo)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Tarjeta>(entity =>
            {
                entity.HasKey(e => e.NumeroTarjeta).HasName("PK__Tarjetas__BC163C0B4444BFF8");

                entity.ToTable("Tarjetas");

                entity.Property(e => e.Pin)
                    .HasMaxLength(3)
                    .IsUnicode(false);
                entity.Property(e => e.NumeroTarjeta)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
