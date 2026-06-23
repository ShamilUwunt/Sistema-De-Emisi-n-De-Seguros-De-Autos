using SegurosLafise.Api.Models;

namespace SegurosLafise.Api.Repositories
{
    public interface IPolizaRepository
    {
        Task<Poliza> AgregarAsync(Poliza poliza);
        Task<Poliza?> ObtenerPorIdAsync(int id);
        Task<List<Poliza>> ObtenerTodasAsync();
        Task<int> ContarPorAnioAsync(int anio);
    }
}
