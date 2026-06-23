using Microsoft.EntityFrameworkCore;
using SegurosLafise.Api.Models;

namespace SegurosLafise.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Cobertura> Coberturas { get; set; }
        public DbSet<Poliza> Polizas { get; set; }
        public DbSet<PolizaCobertura> PolizaCoberturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // La tabla intermedia usa llave primaria compuesta
            modelBuilder.Entity<PolizaCobertura>()
                .HasKey(pc => new { pc.PolizaId, pc.CoberturaId });

            modelBuilder.Entity<PolizaCobertura>()
                .HasOne(pc => pc.Poliza)
                .WithMany(p => p.PolizaCoberturas)
                .HasForeignKey(pc => pc.PolizaId);

            modelBuilder.Entity<PolizaCobertura>()
                .HasOne(pc => pc.Cobertura)
                .WithMany(c => c.PolizaCoberturas)
                .HasForeignKey(pc => pc.CoberturaId);

            // El numero de poliza no se debe repetir
            modelBuilder.Entity<Poliza>()
                .HasIndex(p => p.NumeroPoliza)
                .IsUnique();

            // Datos iniciales para los catalogos
            modelBuilder.Entity<Cobertura>().HasData(
                new Cobertura { Id = 1, Nombre = "Robo Total", MontoCobertura = 15000m },
                new Cobertura { Id = 2, Nombre = "Choque y Colision", MontoCobertura = 8000m },
                new Cobertura { Id = 3, Nombre = "Responsabilidad Civil", MontoCobertura = 5000m },
                new Cobertura { Id = 4, Nombre = "Danos a Terceros", MontoCobertura = 6000m },
                new Cobertura { Id = 5, Nombre = "Asistencia en Carretera", MontoCobertura = 1000m }
            );

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, Nombre = "Juan Perez", Identificacion = "001-200390-1001X", Correo = "juan.perez@correo.com" },
                new Cliente { Id = 2, Nombre = "Maria Lopez", Identificacion = "001-150585-1002Y", Correo = "maria.lopez@correo.com" },
                new Cliente { Id = 3, Nombre = "Carlos Ramirez", Identificacion = "001-100777-1003Z", Correo = "carlos.ramirez@correo.com" }
            );
        }
    }
}
