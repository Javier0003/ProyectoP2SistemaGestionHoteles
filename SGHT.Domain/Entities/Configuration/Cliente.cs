using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities.Configuration
{
    [Table("Cliente", Schema = "dbo")]
    public class Cliente : Auditoria
    {
        [Column("IdCliente")]
        [Key]
        public int IdCliente { get; set; }
        public string? TipoDocumento { get; set; }
        public string? Documento { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Correo { get ; set; }
    }
}
