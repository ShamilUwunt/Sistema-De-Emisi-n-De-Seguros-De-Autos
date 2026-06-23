using SegurosLafise.Api.Models;

namespace SegurosLafise.Api.Services
{
    public interface ICatalogoService
    {
        Task<List<Cliente>> ObtenerClientesAsync();
        Task<List<Cobertura>> ObtenerCoberturasAsync();
    }
}
