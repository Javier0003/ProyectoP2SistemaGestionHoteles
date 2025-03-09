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

        public async Task<OperationResult> GetClienteByDocumento()
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> GetClienteByID(int idCliente)
        {
            OperationResult result = new OperationResult();

            try
            {
                var querys = await (from cliente in _context.Clientes
                                    where cliente.IdCliente == idCliente
                                    select new Cliente()
                                    {
                                        IdCliente = cliente.IdCliente,
                                        TipoDocumento = cliente.TipoDocumento,
                                        Documento = cliente.Documento,
                                        NombreCompleto = cliente.NombreCompleto,
                                        Correo = cliente.Correo,
                                        Estado = cliente.Estado,
                                    }).ToListAsync();
                result.Data = querys;
            }
            catch (Exception ex)
            {
                result.Message = this._configuration["ErroClienteRepository:GetClienteByID"];
                result.Success = false;
                this._logger.LogError(result.Message, ex.ToString());
            }

            if (result.Data == null) 
                return OperationResult.GetErrorResult("No se encontro el cliente", code: 404);
               
       
            return result;
        }

    }
}