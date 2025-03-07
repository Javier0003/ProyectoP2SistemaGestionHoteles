﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Entities.Configuration;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

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

        /*Task<dynamic> IClienteRepository.SaveEntityAsync(Cliente cliente)
        {
            throw new NotImplementedException();
        }*/
    }
}