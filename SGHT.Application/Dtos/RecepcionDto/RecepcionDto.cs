﻿

namespace SGHT.Application.Dtos.RecepcionDto
{
    public class RecepcionDto 
    {
        public DateTime? FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public DateTime? FechaSalidaConfirmacion { get; set; }
        public decimal? PrecioInicial { get; set; }
        public decimal? Adelanto { get; set; }
        public decimal? PrecioRestante { get; set; }
        public decimal? TotalPagado { get; set; }
        public decimal? CostoPenalidad { get; set; }
        public string? Observacion { get; set; }
        public required bool? Estado { get; set; }
        public required int IdCliente { get; set; }
        public required int IdHabitacion { get; set; }

    }
}
