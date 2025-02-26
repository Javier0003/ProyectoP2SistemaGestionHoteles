namespace SGHT.Application.Dtos.Usuarios
{
    public class UsuarioDto
    {
        public required string NombreCompleto { get; set; }
        public required string Correo {  get; set; }
        public int IdRolUsuario { get; set; }
        public required string Clave { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
