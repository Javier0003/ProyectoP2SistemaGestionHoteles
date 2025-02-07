
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    public class Habitacion : Auditoria
    {
        public int IdHabitacion { get; set; }
        public string? Nombre { get; set; }
        public string? Detalle { get; set; }
        public decimal? Precio { get; set; }
    }
}
