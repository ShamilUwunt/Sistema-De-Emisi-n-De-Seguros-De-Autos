using SegurosLafise.Api.Models;

namespace SegurosLafise.Api.Repositories
{
    public interface ICoberturaRepository
    {
        Task<List<Cobertura>> ObtenerTodasAsync();
        Task<List<Cobertura>> ObtenerPorIdsAsync(List<int> ids);
    }
}
