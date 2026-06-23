using SegurosLafise.Api.Dtos;

namespace SegurosLafise.Api.Services
{
    public interface IPolizaService
    {
        Task<PolizaDetalleDto> EmitirAsync(EmitirPolizaDto datos);
        Task<PolizaDetalleDto?> ObtenerPorIdAsync(int id);
        Task<List<PolizaDetalleDto>> ObtenerTodasAsync();
    }
}
