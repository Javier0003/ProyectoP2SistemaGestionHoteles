using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
