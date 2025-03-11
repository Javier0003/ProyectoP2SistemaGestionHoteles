using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Interfaces;
using SGHT.Application.Dtos.RecepcionDto;



namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecepcionController : BaseController
    {
        private readonly IRecepcionService _recepcionService;

        public RecepcionController(IRecepcionService recepcionService, ILogger<RecepcionController> logger)
        {
            _recepcionService = recepcionService;
        }

        [HttpGet("GetRecepcion")]
        public async Task<IActionResult> Get()
        {
            var recepciones = await _recepcionService.GetAll();
            return HandleResponse(recepciones);
        }

        [HttpGet("GetRecepcionByIDCliente")]
        public async Task<IActionResult> GetUsuariosByID(int IDcliente)
        {
            var recepciones = await _recepcionService.GetById(IDcliente);
            return HandleResponse(recepciones);
        }

        [HttpPost("CrearRecepcion")]
        public async Task<IActionResult> Post(SaveRecepcionDto recepcion)
        {
            var recepciones = await _recepcionService.Save(recepcion);
            return HandleResponse(recepciones);
        }

        [HttpDelete("EliminarRecepcion/{id}")]
        public async Task<IActionResult> Delete(DeleteRecepcionDto recepcion)
        {
            var recepciones = await _recepcionService.DeleteById(recepcion);
            return HandleResponse(recepciones);
        }

        [HttpPatch("UpdateRecepcion")]
        public async Task<IActionResult> Put([FromBody] UpdateRecepcionDto recepcion)
        {
            var recepciones = await _recepcionService.UpdateById(recepcion);
            return HandleResponse(recepciones);
        }
    }
}
