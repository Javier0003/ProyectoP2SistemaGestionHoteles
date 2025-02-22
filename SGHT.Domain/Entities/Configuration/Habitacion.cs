using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("Habitacion", Schema = "dbo")]
    public class Habitacion : Auditoria
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
