﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities.Reservation;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;
using SGHT.Model.Model;

namespace SGHT.Persistance.Repositories
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
                                    where cliente.IdCliente == idCliente
                                    select new RecepcionCLienteModel()
                                    {
                                        IdRecepcion = recepcion.IdRecepcion,
                                        IDCliente = cliente.IdCliente,
                                        IDHabitacion = recepcion.IdHabitacion,
                                        Estado = recepcion.Estado,
                                    }).ToListAsync(); // Estamos trabajando en ello.
                result.Data = querys;
            }
            catch (Exception ex)
            {
                result.Message = this._configuration["ErroRecepcionRepository:GetRecepcionByClienteID"];
                result.Success = false;
                this._logger.LogError(result.Message, ex.ToString());
            }
            return result;

        }

        public override async Task<OperationResult> SaveEntityAsync(Recepcion recepcion)
        {
            if(recepcion == null) 
                return OperationResult.GetErrorResult("La recepcion es nula", code: 400);
            
            if(recepcion.Estado == null) 
                return OperationResult.GetErrorResult("El estado de la recepcion no puede ser nulo", code: 400);
            
            if(string.IsNullOrEmpty(recepcion.FechaEntrada.ToString())) 
                return OperationResult.GetErrorResult("La fecha de entrada no puede ser nula", code: 400);
            
            if(string.IsNullOrEmpty(recepcion.FechaSalida.ToString())) 
                return OperationResult.GetErrorResult("La fecha de salida no puede ser nula", code: 400);
            
            if(recepcion.FechaEntrada < DateTime.Today) 
                return OperationResult.GetErrorResult("La fecha de entrada no puede ser anterior a la fecha actual", code: 500);
            
            if(recepcion.FechaSalida < DateTime.Today) 
                return OperationResult.GetErrorResult("La fecha de salida no puede ser anterior a la fecha actual", code: 500);
            
            if(recepcion.FechaSalida < recepcion.FechaEntrada) 
                return OperationResult.GetErrorResult("La fecha de salida no puede ser anterior a la fecha de entrada", code: 500);        
            
            if(_context.Recepcion.Any(rc => rc.IdRecepcion == recepcion.IdRecepcion)) 
                return OperationResult.GetErrorResult("Esta recepcion ya esta registrada en la base de datos", code: 500);
            
            return await base.SaveEntityAsync(recepcion);
        }

       public override async Task<OperationResult> UpdateEntityAsync(Recepcion recepcion)
       {

            if (recepcion is null) return OperationResult.GetErrorResult("La recepcion no puede ser nula");

            if (recepcion.IdRecepcion != recepcion.IdRecepcion)
                return OperationResult.GetErrorResult("Este ID no existe", code: 500);
            
            if (recepcion.IdRecepcion == null)
                return OperationResult.GetErrorResult("El ID no puede ser nulo", code: 400);

            return await base.UpdateEntityAsync(recepcion);
       } 

        public override async Task<OperationResult> DeleteEntityAsync(Recepcion recepcion)
        {
            if (recepcion is null) return OperationResult.GetErrorResult("La recepcion no puede ser nula");
            try
            {
                var result = await GetEntityByIdAsync(recepcion.IdRecepcion);
                if (result == null) return OperationResult.GetErrorResult("No se encontro la recepcion", code: 404);

                result.Estado = false;

                var resultDelete = await UpdateEntityAsync(result);
                if (!resultDelete.Success) return OperationResult.GetErrorResult("No se elimino correctamente", code: 500);

                return OperationResult.GetSuccesResult(resultDelete, "La recepcion se elimino correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionRepository.DeleteEntityAsync: {ex}");
                return OperationResult.GetErrorResult("Error eliminando la recepcion", code: 500);

            }
        }

    }
}
