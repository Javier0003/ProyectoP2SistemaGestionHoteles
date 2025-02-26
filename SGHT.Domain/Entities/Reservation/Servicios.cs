using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("Servicios", Schema = "dbo")]
    public class Servicios 
    {
        [Column("IdServicio")]
        [Key]
        public int IdServicio { get; set; }
        public string? Nombre {get; set; }
        public string? Descripcion { get; set; }
    }
}
