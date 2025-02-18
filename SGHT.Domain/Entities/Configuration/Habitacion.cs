using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGHT.Domain.Entities
{
    [Table("Habitacion", Schema = "dbo")]
    public class Habitacion
    {
        [Column("IdHabitacion")]
        [Key]
        public int IdHabitacion { get; set; }
        public string? Numero { get; set; }
        public string? Detalle { get; set; }
        public decimal? Precio { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
