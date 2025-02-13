using System.ComponentModel.DataAnnotations.Schema;
using SGHT.Domain.Base;

namespace SGHT.Domain.Entities
{
    [Table("EstadoHabitacion", Schema = "dbo")]
    public class EstadoHabitacion : Auditoria
    {
       public int IdEstadoHabitacion { get; set; }
    }
}
