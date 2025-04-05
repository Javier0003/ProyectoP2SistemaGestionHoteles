using Microsoft.AspNetCore.Mvc;
using SGHT.Web.Api.Base;
using SGHT.Model.Model.Recepcion;
using SGHT.Http.Repositories.Interfaces;

namespace SGHT.Web.Api.Controllers
{
    public class RecepcionController : BaseController
    {

        private readonly ILogger<RecepcionController> _logger;
        private readonly IRecepcionHttpRepository _recepcionHttpRepository;

        public RecepcionController(ILogger<RecepcionController> logger, IRecepcionHttpRepository recepcionHttpRepository)
        {
            _recepcionHttpRepository = recepcionHttpRepository;
            _logger = logger;
        }


        // GET: RecepcionController
        public async  Task<IActionResult> Index()
        {
            try
            {
                var res = await _recepcionHttpRepository.SendGetRequestAsync("Recepcion");
                return View(await res.Content.ReadFromJsonAsync<List<GetRecepcionModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionController.Index: {ex}");
                return ViewBagError("Error obteniendo las recepciones.");
            }
        } 
        
        // GET: RecepcionController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await _recepcionHttpRepository.SendGetRequestAsync($"Recepcion/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetRecepcionModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionController.Details: {ex}");
                return ViewBagError("Error obteniendo la recepcion.");
            }
        }

        // GET: RecepcionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecepcionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearRecepcionModel recepcion)
        {
            try
            {
                if (recepcion == null) throw new ArgumentNullException();

                var res = await _recepcionHttpRepository.SendPostRequestAsync("Recepcion/crear", recepcion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionController.Create: {ex}");
                return ViewBagError("Error guardando la recepcion");
            }
        }

        // GET: RecepcionController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await _recepcionHttpRepository.SendGetRequestAsync($"Recepcion/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetRecepcionModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionController.Edit: {ex}");
                return ViewBagError("Error Obteniendo la recepcion");
            }
        }

        // PUT: RecepcionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActualizarRecepcionModel recepcion)
        {
            try
            {
                if (recepcion == null) throw new ArgumentNullException();

                var res = await _recepcionHttpRepository.SendPostRequestAsync("Recepcion/actualizar", recepcion);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError($"clienteController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: RecepcionController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _recepcionHttpRepository.SendGetRequestAsync($"Recepcion/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetRecepcionModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionController.Delete: {ex}");
                return ViewBagError("Error eliminando la recepcion");
            }
        }

        // POST: RecepcionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteRecepcionModel recepcionModel)
        {
            try
            {
                if (recepcionModel == null) throw new ArgumentNullException();

                var res = await _recepcionHttpRepository.SendDeleteRequestAsync("Recepcion/eliminar", recepcionModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionController.Delete: {ex}");
                return ViewBagError("Error eliminando a la recepcion");
            }
        }

    }
}
