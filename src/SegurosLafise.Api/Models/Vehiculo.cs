using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SegurosLafise.Api.Models
{
    // Vehiculo asegurado
    public class Vehiculo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Placa { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Marca { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        public int Anio { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorComercial { get; set; }
    }
}
