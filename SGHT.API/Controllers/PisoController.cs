using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.Piso;
using SGHT.Application.Interfaces;


namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PisoController : BaseController
    {
        private readonly IPisoService _pisoService;

        public PisoController(IPisoService pisoService, ILogger<PisoController> logger)
        {
            _pisoService = pisoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pisoList = await _pisoService.GetAll();
            return HandleResponse(pisoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var piso = await _pisoService.GetById(id);
            if (!piso.Success) return NotFound("floor not found");

            return HandleResponse(piso);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Post(SavePisoDto piso)
        {
            var result = await _pisoService.Save(piso);

            return HandleResponse(result);
        }


        [HttpDelete("eliminar")]
        public async Task<IActionResult> Delete(DeletePisoDto id)
        {
            var result = await _pisoService.DeleteById(id);

            return HandleResponse(result);
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> Put(UpdatePisoDto piso)
        {
            var result = await _pisoService.UpdateById(piso);
            return HandleResponse(result);
        }
    }
}
