using Microsoft.AspNetCore.Mvc;
using SGHT.Model.Model.tarifa;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Web.Api.Controllers
{
    public class TarifaController : BaseController
    {
        private readonly ILogger<TarifaController> _logger;
        public TarifaController(IConfiguration configuration, ILogger<TarifaController> logger, IHttpClientService httpClientService, IErrorHandler errorHandler) : base(configuration, logger, httpClientService, errorHandler)
        {
            _logger = logger;
        }

        // GET: TarifaController
        public async Task<IActionResult> Index()
        {
            try
            {
                var res = await SendGetRequestAsync("Tarifas");
                return View(await res.Content.ReadFromJsonAsync<List<GetTarifaModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifaController.Index: {ex}");
                return ViewBagError("Error obteniendo las tarifas.");
            }
        }

        // GET: TarifaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await SendGetRequestAsync($"Tarifas/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetTarifaModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifaController.Details: {ex}");
                return ViewBagError("Erorr obteniendo la tarifa");
            }
        }

        // GET: TarifaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TarifaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTarifaModel tarifa)
        {
            try
            {
                var res = await SendPostRequestAsync("Tarifas/crear", tarifa);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifaController.Create: {ex}");
                return ViewBagError("Error creando la tarifa");
            }
        }

        // GET: TarifaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await SendGetRequestAsync($"Tarifas/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetTarifaModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifaController.Edit: {ex}");
                return ViewBagError("Error Obteniendo la tarifa");
            }
        }

        // POST: TarifaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetTarifaModel tarifa)
        {
            try
            {
                var res = await SendPatchRequestAsync("Tarifas/actualizar", tarifa);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifaController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: TarifaController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await SendGetRequestAsync($"Tarifas/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetTarifaModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifaController.Delete: {ex}");
                return ViewBagError("Error obteniendo la tarifa");
            }
        }

        // POST: TarifaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteTarifaModel collection)
        {
            try
            {
                var res = await SendDeleteRequestAsync("Tarifas/eliminar", collection);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifaController.Delete: {ex}");
                return ViewBagError("Error eliminando la tarifa");
            }
        }
    }
}
