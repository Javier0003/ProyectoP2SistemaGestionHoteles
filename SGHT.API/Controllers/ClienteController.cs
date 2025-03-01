using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Interfaces;
using SGHT.Application.Dtos.ClienteDto;


namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : BaseController
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService, ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
        }

        [HttpGet("GetClientes")]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _clienteService.GetAll();
            return HandleResponse(clientes);
        }

        [HttpGet("GetClienteById")]
        public async Task<IActionResult> GetClientesByID(int Id)
        {
            var cliente = await _clienteService.GetById(Id);
            return HandleResponse(cliente);
        }

        [HttpPatch("UpdateCliente")]
        public async Task<IActionResult> Put([FromBody] UpdateClienteDto cliente)
        {
            var clientes = await _clienteService.UpdateById(cliente);
            return HandleResponse(clientes);
        }

        [HttpPost("CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] SaveClienteDto cliente)
        {
           var clientes = await _clienteService.Save(cliente);
           return HandleResponse(clientes);
        }

        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> Delete(DeleteClienteDto clienteDto)
        {
           var clientes = await _clienteService.DeleteById(clienteDto);
            return HandleResponse(clientes);
        }
    }
}
