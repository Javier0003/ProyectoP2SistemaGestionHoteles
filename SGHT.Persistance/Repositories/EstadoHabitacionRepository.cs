using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class EstadoHabitacionRepository : BaseRepository<EstadoHabitacion>, IEstadoHabitacionRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<EstadoHabitacionRepository> _logger;
        private readonly IConfiguration _configuration;
        public EstadoHabitacionRepository(SGHTContext context, ILogger<EstadoHabitacionRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetEstadoByIdAsync(int id)
        {
            try
            {
                var estado = await _context.EstadoHabitaciones.FindAsync(id);
                if (estado == null) return OperationResult.GetErrorResult("Estado no encontrado", code: 404);

                return OperationResult.GetSuccesResult(estado, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en GetEstadoByIdAsync: {ex}");
                return OperationResult.GetErrorResult("Error al obtener estado de la habitación", code: 500);
            }
        }

        public  async Task<OperationResult> SaveEstadoAsync(EstadoHabitacion estadoHabitacion)
        {
            if (estadoHabitacion == null) return await OperationResult.GetErrorResultAsync("El estado de la habitación es nulo", code: 400);
            if (string.IsNullOrWhiteSpace(estadoHabitacion.Descripcion)) return await OperationResult.GetErrorResultAsync("La descripción no puede estar vacía", code: 400);

            if (_context.EstadoHabitaciones.Any(e => e.Descripcion == estadoHabitacion.Descripcion))
                return await OperationResult.GetErrorResultAsync("Este estado ya está registrado", code: 400);

            return await base.SaveEntityAsync(estadoHabitacion);
        }
    }
}
   
