using SGHT.Domain.Base;
using SGHT.Domain.Entities.Configuration;
using SGHT.Persistance.Context;

namespace SGHT.Persistance.Validaciones
{
    public class ClienteValidations
    {
        private readonly SGHTContext _context;

        public ClienteValidations(SGHTContext context)
        {
            _context = context;
        }

        public OperationResult Validate(Cliente cliente)
        {
            return ValidateNotNull(cliente)
                .ThenValidate(() => ValidateEstado(cliente))
                .ThenValidate(() => ValidateCorreoWhiteSpace(cliente))
                .ThenValidate(() => ValidateCorreo(cliente))
                .ThenValidate(() => validateNombreWhiteSpace(cliente));
        }

        private OperationResult ValidateNotNull(Cliente cliente)
        {
           return cliente == null 
                ? OperationResult.GetErrorResult("El cliente no puede ser nulo", code: 400)
                : OperationResult.GetSuccesResult(cliente, code: 200);
        }

        private OperationResult ValidateEstado(Cliente cliente)
        {
            return cliente.Estado == null
                ? OperationResult.GetErrorResult("El estado del cliente no puede ser nulo", code: 400)
                : OperationResult.GetSuccesResult(cliente, code: 200);
        }

        private OperationResult ValidateCorreoWhiteSpace(Cliente cliente)
        {
            return string.IsNullOrWhiteSpace(cliente.Correo)
                ? OperationResult.GetErrorResult("El correo del cliente no puede ser nulo o vacio", code: 400)
                : OperationResult.GetSuccesResult(cliente, code: 200);
        }

        private OperationResult ValidateCorreo(Cliente cliente)
        {
            return _context.Clientes.Any(c => c.Correo == cliente.Correo)
                ? OperationResult.GetErrorResult("El correo del cliente ya existe", code: 400)
                : OperationResult.GetSuccesResult(cliente, code: 200);
        }

        private OperationResult validateNombreWhiteSpace(Cliente cliente)
        {
            return string.IsNullOrWhiteSpace(cliente.NombreCompleto)
                ? OperationResult.GetErrorResult("El nombre del cliente no puede ser nulo o vacio", code: 400)
                : OperationResult.GetSuccesResult(cliente, code: 200);
        }

    }

    public static class OperationResultExtensions
    {
        public static OperationResult ThenValidate(this OperationResult result, Func<OperationResult> validate)
        {
            if (!result.Success)
                return result;

            return validate();
        }
    }
}
