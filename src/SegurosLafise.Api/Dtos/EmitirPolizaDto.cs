using System.ComponentModel.DataAnnotations;

namespace SegurosLafise.Api.Dtos
{
    // Lo que recibe el endpoint POST /api/polizas/emitir
    public class EmitirPolizaDto
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public VehiculoDto Vehiculo { get; set; } = new VehiculoDto();

        // IDs de las coberturas seleccionadas
        [Required]
        [MinLength(1, ErrorMessage = "Debe seleccionar al menos una cobertura")]
        public List<int> CoberturasIds { get; set; } = new List<int>();
    }
}
