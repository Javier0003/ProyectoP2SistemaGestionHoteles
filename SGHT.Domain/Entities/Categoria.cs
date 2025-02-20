using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("Categoria", Schema = "dbo")]
    public class Categoria : Auditoria
    {
        [Column("IdCategoria")]
        [Key]
        public int IdCategoria { get; set; }
    }
}
