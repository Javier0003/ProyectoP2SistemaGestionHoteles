
namespace SGHT.Model.Model
{
    public class RecepcionCLienteModel
    {
        public int IdRecepcion { get; set; }
        public int IDCliente { get; set; }
        public string? NombreCliente { get; set;}
        public decimal? TotalPagado { get; set;}
        public bool? Estado {  get; set; }
    }
}
