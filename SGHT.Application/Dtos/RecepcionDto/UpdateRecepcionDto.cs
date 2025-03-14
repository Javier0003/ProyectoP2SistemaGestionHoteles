

namespace SGHT.Application.Dtos.RecepcionDto
{
    public class UpdateRecepcionDto : RecepcionDto
    {
        public int IdRecepcion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
    }
}
