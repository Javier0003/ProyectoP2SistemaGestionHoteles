
namespace SGHT.Model.Model.categoria
{
    public class UpdateCategoriaModel
    {
        public int IdCategoria { get; set; }
        public int IdServicio { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
