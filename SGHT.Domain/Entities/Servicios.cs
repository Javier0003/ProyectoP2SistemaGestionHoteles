using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("Servicios", Schema = "dbo")]
    public class Servicios : Auditoria
    {
        [Column("IdServicio")]
        [Key]
        public int IdServicio { get; set; }
        public string? Nombre {get; set; }

    }
}
