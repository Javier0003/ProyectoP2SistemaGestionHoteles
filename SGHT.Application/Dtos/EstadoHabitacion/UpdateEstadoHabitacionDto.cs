namespace SGHT.Application.Dtos.EstadoHabitacion
{
    public class UpdateEstadoHabitacionDto : EstadoHabitacionDto
    {
       
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }
}
