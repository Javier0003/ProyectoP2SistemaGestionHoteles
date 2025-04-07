using Microsoft.AspNetCore.Mvc;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Model.Model.Piso;
using SGHT.Web.Api.Base;

namespace SGHT.Web.Api.Controllers
{
    public class PisoController : BaseController
    {
        private readonly ILogger<PisoController> _logger;
        private readonly IPisoHttpRepository _pisoHttpRepository;

        public PisoController(ILogger<PisoController> logger, IPisoHttpRepository pisoHttpRepository)
        {
            _logger = logger;
            _pisoHttpRepository = pisoHttpRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var res = await _pisoHttpRepository.SendGetRequestAsync("Piso");
                return View(await res.Content.ReadFromJsonAsync<List<GetPisoModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoController.Index: {ex}");
                return ViewBagError("Error obteniendo los pisos.");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await _pisoHttpRepository.SendGetRequestAsync($"Piso/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetPisoModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoController.Details: {ex}");
                return ViewBagError("Error obteniendo el piso.");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GetPisoModel piso)
        {
            try
            {
                if (piso == null) throw new ArgumentNullException();

                var res = await _pisoHttpRepository.SendPostRequestAsync("Piso/crear", piso);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoController.Create: {ex}");
                return ViewBagError("Error creando el piso.");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await _pisoHttpRepository.SendGetRequestAsync($"Piso/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetPisoModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoController.Edit: {ex}");
                return ViewBagError("Error obteniendo el piso.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetPisoModel piso)
        {
            try
            {
                if (piso == null) throw new ArgumentNullException();

                var res = await _pisoHttpRepository.SendPatchRequestAsync("Piso/actualizar", piso);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _pisoHttpRepository.SendGetRequestAsync($"Piso/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetPisoModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoController.Delete: {ex}");
                return ViewBagError("Error obteniendo el piso.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeletePisoModel piso)
        {
            try
            {
                if (piso == null) throw new ArgumentNullException();

                var res = await _pisoHttpRepository.SendDeleteRequestAsync("Piso/eliminar", piso);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoController.Delete: {ex}");
                return ViewBagError("Error eliminando el piso.");
            }
        }
    }
}
