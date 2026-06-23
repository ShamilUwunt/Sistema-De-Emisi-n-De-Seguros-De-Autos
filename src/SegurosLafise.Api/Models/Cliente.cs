using System.ComponentModel.DataAnnotations;

namespace SegurosLafise.Api.Models
{
    // Cliente que contrata la poliza
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        // DNI o RUC
        [Required]
        [MaxLength(20)]
        public string Identificacion { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        // Una persona puede tener varias polizas
        public List<Poliza> Polizas { get; set; } = new List<Poliza>();
    }
}
