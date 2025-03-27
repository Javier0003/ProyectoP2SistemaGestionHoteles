namespace SGHT.Model.Model.tarifa
{
    public class ActualizarTarifaModel
    {
        public int IdHabitacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int PrecioPorNoche { get; set; }
        public int Descuento { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int IdTarifa { get; set; }
    }
}
