namespace SGHT.Model.Model.rolUsuario
{
    public class GetRolUsuarioModel
    {
        public int IdRolUsuario { get; set; }
        public DateTimeOffset FechaCreacion { get; set; }
        public bool Estado { get; set; }
        public string Descripcion { get; set; }
    }
}
