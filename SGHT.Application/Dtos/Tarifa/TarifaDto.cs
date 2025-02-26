namespace SGHT.Application.Dtos.Tarifa
{
    public class TarifaDto
    {
        public int IdHabitacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public decimal? Descuento { get; set; }
        public required string Descripcion { get; set; }
        public required string Estado { get; set; }
    }
}
