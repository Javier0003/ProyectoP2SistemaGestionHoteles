using System;

namespace SGHT.Application.Dtos.EstadoHabitacion;

public class SaveEstadoHabitacionDto : EstadoHabitacionDto
{
	public DateTime FechaCreacion { get; set; }
    
    public bool Estado { get; set; }
}
