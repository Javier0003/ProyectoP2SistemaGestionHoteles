namespace SGHT.Application.Dtos.Habitacion
{
    public class HabitacionDto
    {
        public string? Numero { get; set; }
        public string? Detalle { get; set; }
        public decimal Precio { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
