

using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("RolUsuario", Schema = "dbo")]
    public class RolUsuario : Auditoria
    {
        public int IdRolUsuarios { get; set; }
    }
}
