namespace SGHT.Model.Model.usuario
{
    public class GetUsuarioModel
    {
        public string NombreCompleto { get; set; }

        public string Correo { get; set; }

        public int IdRolUsuario { get; set; }

        public string Clave { get; set; }

        public bool Estado { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
