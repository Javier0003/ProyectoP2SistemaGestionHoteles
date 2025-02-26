using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.Tarifa;
using SGHT.Application.Interfaces;

namespace SGHT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifasController : BaseController
    {
        private readonly ITarifaService _tarifasService;

        public TarifasController(ITarifaService tarifasService, ILogger<TarifasController> logger)
        {
            _tarifasService = tarifasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTarifas()
        {
            var tarifas = await _tarifasService.GetAll();
            return HandleResponse(tarifas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarifaById(int id)
        {
            var tarifa = await _tarifasService.GetById(id);
            return HandleResponse(tarifa);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CreateTarifa(SaveTarifaDto tarifas)
        {
            var result = await _tarifasService.Save(tarifas);

            return HandleResponse(result);
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> EliminarTarifa(DeleteTarifaDto dto)
        {
            var result = await _tarifasService.DeleteById(dto);
            return HandleResponse(result);
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> ActualizarTarifa(UpdateTarifaDto tarifas)
        {
            var result = await _tarifasService.UpdateById(tarifas);

            return HandleResponse(result);
        }
    }
}
