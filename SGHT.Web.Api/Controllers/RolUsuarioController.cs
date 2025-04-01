using Microsoft.AspNetCore.Mvc;
using SGHT.Model.Model.rolUsuario;
using SGHT.Model.Model.tarifa;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Web.Api.Controllers
{
    public class RolUsuarioController : BaseController
    {
        private readonly ILogger<RolUsuarioController> _logger;
        public RolUsuarioController(IConfiguration configuration, ILogger<RolUsuarioController> logger, IHttpClientService httpClientService, IErrorHandler errorHandler) : base(configuration, logger, httpClientService, errorHandler)
        {
            _logger = logger;
        }

        // GET: RolUsuarioController
        public async Task<IActionResult> Index()
        {
            try
            {
                var res = await SendGetRequestAsync("RolUsuario");
                return View(await res.Content.ReadFromJsonAsync<List<GetRolUsuarioModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"RolUsuarioController.Index: {ex}");
                return ViewBagError("Error obteniendo los roles.");
            }
        }

        // GET: RolUsuarioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await SendGetRequestAsync($"RolUsuario/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetRolUsuarioModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"RolUsuarioController.Details: {ex}");
                return ViewBagError("Error obteniendo el Rol");
            }
        }

        // GET: RolUsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolUsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRolUsuarioModel rol)
        {
            try
            {
                var res = await SendPostRequestAsync("RolUsuario/crear", rol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"RolUsuarioController.Create: {ex}");
                return ViewBagError("Error creando el rol");
            }
        }

        // GET: RolUsuarioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await SendGetRequestAsync($"RolUsuario/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetRolUsuarioModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"RolUsuarioController.Edit: {ex}");
                return ViewBagError("Error Obteniendo el rol");
            }
        }

        // POST: RolUsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetRolUsuarioModel rol)
        {
            try
            {
                var res = await SendPatchRequestAsync("RolUsuario/actualizar", rol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"RolUsuarioController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: RolUsuarioController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await SendGetRequestAsync($"RolUsuario/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetTarifaModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"RolUsuarioController.Delete: {ex}");
                return ViewBagError("Error obteniendo el rol");
            }
        }

        // POST: RolUsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteRolUsuarioModel collection)
        {
            try
            {
                collection.IdRolUsuario = id;
                var res = await SendDeleteRequestAsync("RolUsuario/eliminar", collection);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"RolUsuarioController.Delete: {ex}");
                return ViewBagError("Error eliminando el rol");
            }
        }
    }
}
