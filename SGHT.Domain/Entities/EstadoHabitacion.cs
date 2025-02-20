using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("EstadoHabitacion", Schema = "dbo")]
    public class EstadoHabitacion : Auditoria
    {
        [Column("IdEstadoHabitacion")]
        [Key]
       public int IdEstadoHabitacion { get; set; }
    }
}
