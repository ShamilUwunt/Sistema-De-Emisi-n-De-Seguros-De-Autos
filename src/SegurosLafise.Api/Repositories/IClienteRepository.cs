using SegurosLafise.Api.Models;

namespace SegurosLafise.Api.Repositories
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObtenerTodosAsync();
        Task<Cliente?> ObtenerPorIdAsync(int id);
    }
}
