
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("Categoria", Schema = "dbo")]
    public class Catetgoria : Auditoria
    {
        public int IdCategoria { get; set; }
        
    }
}
