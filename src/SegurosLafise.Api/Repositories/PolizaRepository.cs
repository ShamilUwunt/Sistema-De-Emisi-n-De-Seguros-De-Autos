using Microsoft.EntityFrameworkCore;
using SegurosLafise.Api.Data;
using SegurosLafise.Api.Models;

namespace SegurosLafise.Api.Repositories
{
    public class PolizaRepository : IPolizaRepository
    {
        private readonly AppDbContext _context;

        public PolizaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Poliza> AgregarAsync(Poliza poliza)
        {
            _context.Polizas.Add(poliza);
            await _context.SaveChangesAsync();
            return poliza;
        }

        public async Task<Poliza?> ObtenerPorIdAsync(int id)
        {
            return await _context.Polizas
                .Include(p => p.Cliente)
                .Include(p => p.Vehiculo)
                .Include(p => p.PolizaCoberturas)
                    .ThenInclude(pc => pc.Cobertura)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Poliza>> ObtenerTodasAsync()
        {
            return await _context.Polizas
                .Include(p => p.Cliente)
                .Include(p => p.Vehiculo)
                .Include(p => p.PolizaCoberturas)
                    .ThenInclude(pc => pc.Cobertura)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<int> ContarPorAnioAsync(int anio)
        {
            return await _context.Polizas
                .CountAsync(p => p.FechaEmision.Year == anio);
        }
    }
}
