using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("RolUsuario", Schema = "dbo")]
    public class RolUsuario : Auditoria
    {
        [Column("IdRolUsuario")]
        [Key]
        public int IdRolUsuario { get; set; }
        public string? Descripcion { get; set; }

    }
}
