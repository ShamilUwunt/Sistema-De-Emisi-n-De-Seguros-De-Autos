namespace SegurosLafise.Api.Models
{
    // Tabla intermedia entre Poliza y Cobertura (relacion muchos a muchos)
    public class PolizaCobertura
    {
        public int PolizaId { get; set; }
        public Poliza? Poliza { get; set; }

        public int CoberturaId { get; set; }
        public Cobertura? Cobertura { get; set; }
    }
}
