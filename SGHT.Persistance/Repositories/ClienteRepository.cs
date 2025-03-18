using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Entities.Configuration;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;
using Azure.Core;


namespace SGHT.Persistance.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<ClienteRepository> _logger;
        private readonly IConfiguration _configuration;

        public ClienteRepository(SGHTContext context, ILogger<ClienteRepository> logger, IConfiguration configuration) : base(context) 
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        //public async Task<OperationResult> GetClienteByID(int idCliente)
        //{
        //    OperationResult result = new OperationResult();

        //    try
        //    {
        //        var querys = await (from cliente in _context.Clientes
        //                            where cliente.IdCliente == idCliente
        //                            select new Cliente()
        //                            {
        //                                IdCliente = cliente.IdCliente,
        //                                TipoDocumento = cliente.TipoDocumento,
        //                                Documento = cliente.Documento,
        //                                NombreCompleto = cliente.NombreCompleto,
        //                                Correo = cliente.Correo,
        //                                Estado = cliente.Estado,
        //                            }).ToListAsync();
        //        result.Data = querys;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = this._configuration["ErroClienteRepository:GetClienteByID"];
        //        result.Success = false;
        //        this._logger.LogError(result.Message, ex.ToString());
        //    }

        //    if (result.Data == null) 
        //        return OperationResult.GetErrorResult("No se encontro el cliente", code: 404);
               
       
        //    return result;
        //}

        public override async Task<OperationResult> SaveEntityAsync(Cliente cliente)
        {
            if(cliente == null) 
                return OperationResult.GetErrorResult("El cliente es nulo", code: 400);
            
            if(cliente.Estado == null) 
                return OperationResult.GetErrorResult("El estado del cliente no puede ser nulo", code: 400);
            
            return await base.SaveEntityAsync(cliente);
        }

        public override async Task<OperationResult> UpdateEntityAsync(Cliente cliente)
        {
            if(cliente == null) 
                return OperationResult.GetErrorResult("El cliente es nulo", code: 400);
            
            if(cliente.Estado == null) 
                return OperationResult.GetErrorResult("El estado del cliente no puede ser nulo", code: 400);
            
            return await base.UpdateEntityAsync(cliente);
        }

        public override async Task<OperationResult> DeleteEntityAsync(Cliente cliente)
        {
            if (cliente.IdCliente == null)
                return OperationResult.GetErrorResult("El estado del cliente no puede ser nulo", code: 500);

            return await base.DeleteEntityAsync(cliente);
        }

    }
}