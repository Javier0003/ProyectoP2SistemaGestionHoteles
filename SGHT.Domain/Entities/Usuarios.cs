using SGHT.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGHT.Domain.Entities
{
    [Table("Usuario", Schema="dbo")]
    public class Usuarios : Auditoria
    {
        [Column("IdUsuario")]
        [Key]
        public int IdUsuario { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
        public int IdRolUsuario { get; set; }
    }
}
