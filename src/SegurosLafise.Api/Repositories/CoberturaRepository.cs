using Microsoft.EntityFrameworkCore;
using SegurosLafise.Api.Data;
using SegurosLafise.Api.Models;

namespace SegurosLafise.Api.Repositories
{
    public class CoberturaRepository : ICoberturaRepository
    {
        private readonly AppDbContext _context;

        public CoberturaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cobertura>> ObtenerTodasAsync()
        {
            return await _context.Coberturas.ToListAsync();
        }

        public async Task<List<Cobertura>> ObtenerPorIdsAsync(List<int> ids)
        {
            return await _context.Coberturas
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }
    }
}
