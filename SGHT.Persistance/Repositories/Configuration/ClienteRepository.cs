using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Configuration;


namespace SGHT.Persistance.Repositories.Configuration
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

        public override Task<OperationResult> SaveEntityAsync(Cliente cliente)
        {
            return base.SaveEntityAsync(cliente);
        }

        public override Task<OperationResult> UpdateEntityAsync(Cliente cliente)
        {
            return base.UpdateEntityAsync(cliente);
        }
    }
}