using Microsoft.AspNetCore.Mvc;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Model.Model.servicio;
using SGHT.Web.Api.Base;

namespace SGHT.Web.Api.Controllers
{
    public class ServicioController : BaseController
    {
        private readonly ILogger<ServicioController> _logger;
        private readonly IServiciosHttpRepository _serviciosHttpRepository;
        public ServicioController(ILogger<ServicioController> logger, IServiciosHttpRepository serviciosHttpRepository)
        {
            _serviciosHttpRepository = serviciosHttpRepository;
            _logger = logger;
        }

        // GET: ServicioController
        public async Task<IActionResult> Index()
        {
            try
            {
                var res = await _serviciosHttpRepository.SendGetRequestAsync("Servicios");
                return View(await res.Content.ReadFromJsonAsync<List<GetServicioModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServiciosController.Index: {ex}");
                return ViewBagError("Error obteniendo los servicios.");
            }
        }

        // GET: ServiciosController/Details/2
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await _serviciosHttpRepository.SendGetRequestAsync($"Servicios/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetServicioModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServicioController.Index: {ex}");
                return ViewBagError("Error obteniendo los servicios.");
            }
        }

        // GET: ServiciosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServicioModel servicio)
        {
            try
            {
                if (servicio == null)
                    throw new ArgumentNullException();

                var res = await _serviciosHttpRepository.SendPostRequestAsync("Servicio/crear", servicio);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServicioController.Index: {ex}");
                return ViewBagError("Error creando el servicio.");
            }
        }

        // GET: ServicioController/Edit/2
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await _serviciosHttpRepository.SendGetRequestAsync($"Servicio/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetServicioModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServicioController.Index: {ex}");
                return ViewBagError("Error obteniendo el servicio.");
            }
        }

        // POST: ServicioController/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetServicioModel servicio)
        {
            try
            {
                if (servicio == null)
                    throw new ArgumentNullException();

                var res = await _serviciosHttpRepository.SendPatchRequestAsync("Servicio/actualizar", servicio);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServicioController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: ServicioController/Delete/2
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _serviciosHttpRepository.SendGetRequestAsync($"Servicio/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetServicioModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServicioController.Index: {ex}");
                return ViewBagError("Error obteniendo el servicio.");
            }
        }

        // POST: ServicioController/Delete/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteServicioModel servicio)
        {
            try
            {
                if (servicio == null) throw new ArgumentNullException();

                var res = await _serviciosHttpRepository.SendDeleteRequestAsync("Servicio/eliminar", servicio);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServicioController.Delete: {ex}");
                return ViewBagError("Error eliminando el servicio");
            }
        }
    }
}