using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Configuration;

namespace SGHT.Persistance.Repositories.Configuration
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(SGHTContext context) : base(context)
        {

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