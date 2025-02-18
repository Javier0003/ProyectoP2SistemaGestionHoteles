using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("Tarifas", Schema = "dbo")]
    public class Tarifas : Auditoria
    {
        [Key]
        public int IdTarifas { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin {  get; set; }
        public decimal? PrecioPorNoche { get; set; }
        public decimal? Descuento { get; set; }
    }
}
