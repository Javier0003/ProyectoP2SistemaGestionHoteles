using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Configuration;

namespace SGHT.Persistance.Repositories
{
    public class CategoriaRepository : BaseRepository<Cliente>, IClienteRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<CategoriaRepository> _logger;
        private readonly IConfiguration _configuration;

        public CategoriaRepository(SGHTContext context, ILogger<CategoriaRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
    }
}
