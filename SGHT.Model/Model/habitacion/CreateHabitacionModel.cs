
namespace SGHT.Model.Model.habitacion
{
    public class CreateHabitacionModel
    {
        public string? Numero { get; set; }
        public string? Detalle { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
