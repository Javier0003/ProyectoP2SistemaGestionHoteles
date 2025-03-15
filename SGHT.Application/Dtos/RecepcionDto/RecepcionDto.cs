

using System.Data.SqlTypes;

namespace SGHT.Application.Dtos.RecepcionDto
{
    public class RecepcionDto 
    {
        public RecepcionDto()
        {
            Estado = true;
        }

        public int IdRecepcion { get; set; }
        public int IdCliente { get; set; }
        public int IdHabitacion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaSalidaConfirmacion { get; set; }
        public SqlMoney PrecioInicial { get; set; }
        public SqlMoney Adelanto { get; set; }
        public SqlMoney PrecioRestante { get; set; }
        public SqlMoney TotalPagado { get; set; }
        public SqlMoney CostoPenalidad { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }

      //        ,[IdCliente]
      //,[IdHabitacion]
      //,[FechaEntrada]
      //,[FechaSalida]
      //,[FechaSalidaConfirmacion]
      //,[PrecioInicial]
      //,[Adelanto]
      //,[PrecioRestante]
      //,[TotalPagado]
      //,[CostoPenalidad]
      //,[Observacion]
      //,[Estado]


    }
}
