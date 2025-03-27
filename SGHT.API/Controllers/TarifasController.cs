using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.Tarifa;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;

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
            try
            {
                var tarifas = await _tarifasService.GetAll();
                return HandleResponse(tarifas);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarifaById(int id)
        {
            if (!IsIdValid(id)) return BadRequest("id is not valid");
            try
            {
                var tarifa = await _tarifasService.GetById(id);
                return HandleResponse(tarifa);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CreateTarifa(SaveTarifaDto tarifas)
        {
            if (!IsIdValid(tarifas.IdHabitacion)) return BadRequest("invalid roomid");
            try
            {
                var result = await _tarifasService.Save(tarifas);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> EliminarTarifa(DeleteTarifaDto dto)
        {
            if (!IsIdValid(dto.IdTarifa)) return BadRequest("invalid id");
            try
            {
                var result = await _tarifasService.DeleteById(dto);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> ActualizarTarifa(UpdateTarifaDto tarifas)
        {
            if (!IsIdValid(tarifas.IdTarifa)) return BadRequest("invalid id");
            try
            {
                var result = await _tarifasService.UpdateById(tarifas);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }
    }
}
