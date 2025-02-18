using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("Servicios", Schema = "dbo")]
    public class Servicios : Auditoria
    {
        [Key]
        public int IdServicios { get; set; }
        public string? Nombre {get; set; }
    }
}
