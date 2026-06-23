using Microsoft.AspNetCore.Mvc;
using SegurosLafise.Api.Services;

namespace SegurosLafise.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogosController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogosController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        // GET /api/catalogos/clientes
        [HttpGet("clientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            var clientes = await _catalogoService.ObtenerClientesAsync();
            // Se devuelve solo lo necesario para el catalogo
            var resultado = clientes.Select(c => new
            {
                c.Id,
                c.Nombre,
                c.Identificacion,
                c.Correo
            });
            return Ok(resultado);
        }

        // GET /api/catalogos/coberturas
        [HttpGet("coberturas")]
        public async Task<IActionResult> ObtenerCoberturas()
        {
            var coberturas = await _catalogoService.ObtenerCoberturasAsync();
            var resultado = coberturas.Select(c => new
            {
                c.Id,
                c.Nombre,
                c.MontoCobertura
            });
            return Ok(resultado);
        }
    }
}
