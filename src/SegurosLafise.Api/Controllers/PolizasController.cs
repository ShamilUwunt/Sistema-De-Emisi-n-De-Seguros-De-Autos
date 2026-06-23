using Microsoft.AspNetCore.Mvc;
using SegurosLafise.Api.Dtos;
using SegurosLafise.Api.Services;

namespace SegurosLafise.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolizasController : ControllerBase
    {
        private readonly IPolizaService _polizaService;

        public PolizasController(IPolizaService polizaService)
        {
            _polizaService = polizaService;
        }

        // POST /api/polizas/emitir
        [HttpPost("emitir")]
        public async Task<IActionResult> Emitir([FromBody] EmitirPolizaDto datos)
        {
            // Si el modelo no cumple las validaciones -> 400
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var poliza = await _polizaService.EmitirAsync(datos);
                // 201 Created con la ubicacion del recurso nuevo
                return CreatedAtAction(nameof(ObtenerPorId), new { id = poliza.Id }, poliza);
            }
            catch (NegocioException ex)
            {
                if (ex.EsNoEncontrado)
                {
                    return NotFound(new { mensaje = ex.Message });
                }
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET /api/polizas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var poliza = await _polizaService.ObtenerPorIdAsync(id);
            if (poliza == null)
            {
                return NotFound(new { mensaje = $"No se encontro la poliza con Id {id}." });
            }
            return Ok(poliza);
        }

        // GET /api/polizas
        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var polizas = await _polizaService.ObtenerTodasAsync();
            return Ok(polizas);
        }
    }
}
