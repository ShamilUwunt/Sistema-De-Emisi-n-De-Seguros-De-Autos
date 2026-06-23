namespace SegurosLafise.Api.Dtos
{
    // Respuesta con el detalle de una poliza
    public class PolizaDetalleDto
    {
        public int Id { get; set; }
        public string NumeroPoliza { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public decimal SumaAsegurada { get; set; }
        public decimal PrimaTotal { get; set; }

        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteIdentificacion { get; set; } = string.Empty;

        public string VehiculoPlaca { get; set; } = string.Empty;
        public string VehiculoMarca { get; set; } = string.Empty;
        public string VehiculoModelo { get; set; } = string.Empty;
        public int VehiculoAnio { get; set; }

        public List<CoberturaDto> Coberturas { get; set; } = new List<CoberturaDto>();
    }

    public class CoberturaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal MontoCobertura { get; set; }
    }
}
