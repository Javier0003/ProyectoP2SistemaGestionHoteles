
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    public class Usuarios : Auditoria
    {
        public int IdUsuarios { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
    }
}
