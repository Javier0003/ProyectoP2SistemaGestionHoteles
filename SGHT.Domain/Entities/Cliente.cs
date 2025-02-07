using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    public class Cliente : Auditoria
    {
        public int IdCliente { get; set; }
        public string? TipoDocumento { get; set; }
        public string? Documento { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Correo { get ; set; }
    }
}
