using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class UsuariosRepository : BaseRepository<Usuarios>, IUsuariosRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<UsuariosRepository> _logger;
        private readonly IConfiguration _configuration;
        public UsuariosRepository(SGHTContext context, ILogger<UsuariosRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
    }
}
