using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("Piso", Schema = "dbo")]
    public class Piso : Auditoria
    {
        public int IdPiso { get; set; }

    }
}
