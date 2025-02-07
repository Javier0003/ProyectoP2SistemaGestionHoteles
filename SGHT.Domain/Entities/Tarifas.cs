
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    public class Tarifas : Auditoria
    {
        public int IdTarifas { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin {  get; set; }
        public decimal? PrecioPorNoche { get; set; }
        public decimal? Descuento { get; set; }
    }
}
