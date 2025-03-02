

namespace SGHT.Application.Dtos.Categoria
{
    public class CategoriaDto
    {
        public int IdServicio { get; set; }
        public required string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
} 

