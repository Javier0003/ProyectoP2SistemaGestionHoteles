using SGHT.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGHT.Domain.Entities
{
    [Table("Usuario", Schema="dbo")]
    public class Usuarios : Auditoria
    {
        public int IdUsuarios { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
    }
}
