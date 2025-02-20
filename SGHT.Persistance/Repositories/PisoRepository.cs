using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Configuration;

namespace SGHT.Persistance.Repositories.Configuration
{
    public class PisoRepository : BaseRepository<Piso>, IPisoRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<PisoRepository> _logger;
        private readonly IConfiguration _configuration;
        public PisoRepository(SGHTContext context, ILogger<PisoRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
    }
}
