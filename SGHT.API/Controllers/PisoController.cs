using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.Piso;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;

namespace SGHT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PisoController : BaseController
    {
        private readonly IPisoService _pisoService;

        public PisoController(IPisoService pisoService, ILogger<PisoController> logger)
        {
            _pisoService = pisoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPisos()
        {
            try
            {
                var pisos = await _pisoService.GetAll();
                return HandleResponse(pisos);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPisoById(int id)
        {
            if (!IsIdValid(id)) return BadRequest("Invalid id");
            try
            {
                var piso = await _pisoService.GetById(id);
                return HandleResponse(piso);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CreatePiso(SavePisoDto pisoDto)
        {
            try
            {
                var result = await _pisoService.Save(pisoDto);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> EliminarPiso(DeletePisoDto dto)
        {
            if (!IsIdValid(dto.IdPiso)) return BadRequest("Invalid id");
            try
            {
                var result = await _pisoService.DeleteById(dto);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> ActualizarPiso(UpdatePisoDto pisoDto)
        {
            if (!IsIdValid(pisoDto.IdPiso)) return BadRequest("Invalid id");
            try
            {
                var result = await _pisoService.UpdateById(pisoDto);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }
    }
}
