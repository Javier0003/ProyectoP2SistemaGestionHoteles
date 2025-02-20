using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("Tarifas", Schema = "dbo")]
    public class Tarifas
    {
        [Column("IdTarifa")]
        [Key]
        public int IdTarifa { get; set; }
        public int IdHabitacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? PrecioPorNoche { get; set; }
        public decimal? Descuento { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }
    }
}