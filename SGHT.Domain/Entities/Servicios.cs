
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    public class Servicios : Auditoria
    {
        public int IdServicios { get; set; }
        public string? Nombre { get; set; }
    }
}
