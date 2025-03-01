

namespace SGHT.Application.Dtos.ClienteDto
{
    public class ClienteDto 
    {
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public required string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
    }
}