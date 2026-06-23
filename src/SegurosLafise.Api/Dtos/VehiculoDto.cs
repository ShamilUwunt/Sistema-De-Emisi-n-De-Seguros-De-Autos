using System.ComponentModel.DataAnnotations;

namespace SegurosLafise.Api.Dtos
{
    // Datos del auto que se mandan al emitir la poliza
    public class VehiculoDto
    {
        [Required]
        [MaxLength(10)]
        public string Placa { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Marca { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; } = string.Empty;

        [Range(1950, 2100)]
        public int Anio { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El valor comercial debe ser mayor o igual a 0")]
        public decimal ValorComercial { get; set; }
    }
}
