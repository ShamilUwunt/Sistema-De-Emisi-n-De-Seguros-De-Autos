using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SegurosLafise.Api.Models
{
    // Poliza emitida
    public class Poliza
    {
        public int Id { get; set; }

        // Se genera automatico en el servidor (ej: POL-2026-00001)
        [MaxLength(30)]
        public string NumeroPoliza { get; set; } = string.Empty;

        // FK Cliente
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        // FK Vehiculo
        public int VehiculoId { get; set; }
        public Vehiculo? Vehiculo { get; set; }

        public DateTime FechaEmision { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SumaAsegurada { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrimaTotal { get; set; }

        // Coberturas que lleva la poliza (N a N)
        public List<PolizaCobertura> PolizaCoberturas { get; set; } = new List<PolizaCobertura>();
    }
}
