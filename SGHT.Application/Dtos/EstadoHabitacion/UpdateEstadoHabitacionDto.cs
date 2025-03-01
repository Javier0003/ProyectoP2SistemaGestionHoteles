namespace SGHT.Application.Dtos.EstadoHabitacion
{
    public class UpdateEstadoHabitacionDto : EstadoHabitacionDto
    {
        public int IdEstadoHabitacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }
}
