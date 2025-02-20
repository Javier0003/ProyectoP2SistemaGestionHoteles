using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Model.Model;
using SGHT.Persistance.Interfaces.Reservation;

namespace SGHT.Persistance.Repositories.Reservation
{
    public class RecepcionRepository : BaseRepository<Recepcion>, IRecepcionRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<RecepcionRepository> _logger;
        private readonly IConfiguration _configuration;
        public RecepcionRepository(SGHTContext context, ILogger<RecepcionRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetRecepcionByClienteID(int idCliente)
        {
            OperationResult result = new OperationResult();

            try
            {
                var querys = await (from recepcion in _context.Recepcion
                                    join cliente in _context.Clientes on recepcion.IdRecepcion equals cliente.IdCliente
                                    where recepcion.IdRecepcion == idCliente
                                    select new RecepcionCLienteModel()
                                    {
                                        IdRecepcion = recepcion.IdRecepcion,
                                        IDCliente = cliente.IdCliente,
                                        NombreCliente = cliente.NombreCompleto,
                                        TotalPagado = recepcion.TotalPagado,
                                        Estado = recepcion.Estado,
                                    }).ToListAsync();
                result.Data = querys;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErroRecepcionRepository:GetRecepcionByClienteID"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;

        }

        public override Task<OperationResult> SaveEntityAsync(Recepcion recepcion)
        {
            return base.SaveEntityAsync(recepcion);
        }

        public override Task<OperationResult> UpdateEntityAsync(Recepcion recepcion)
        {
            return base.UpdateEntityAsync(recepcion);
        }
    }
}
