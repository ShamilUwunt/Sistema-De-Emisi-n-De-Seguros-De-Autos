using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SegurosLafise.Api.Models
{
    // Catalogo de coberturas (Robo, Choque, Responsabilidad Civil, etc)
    public class Cobertura
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoCobertura { get; set; }

        // Relacion N a N con polizas
        public List<PolizaCobertura> PolizaCoberturas { get; set; } = new List<PolizaCobertura>();
    }
}
