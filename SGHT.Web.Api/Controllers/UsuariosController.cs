using Microsoft.AspNetCore.Mvc;
using SGHT.Model.Model.usuario;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Web.Api.Controllers
{
    public class UsuariosController : BaseController
    {
        private readonly ILogger<UsuariosController> _logger;
        public UsuariosController(IConfiguration configuration, ILogger<UsuariosController> logger, IHttpClientService httpClientService, IErrorHandler errorHandler) : base(configuration, logger, httpClientService, errorHandler) 
        {
            _logger = logger;
        }

        // GET: UsuariosController
        public async Task<IActionResult> Index()
        {
            try
            {
                var res = await SendGetRequestAsync("Usuario");
                return View(await res.Content.ReadFromJsonAsync<List<GetUsuarioModel>>());
            }
            catch (Exception ex) 
            {
                _logger.LogError($"UsuariosController.Index: {ex}");
                return ViewBagError("Error obteniendo los usuarios.");
            }
        }

        // GET: RolUsuarioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await SendGetRequestAsync($"Usuario/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetUsuarioModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"UsuariosController.Details: {ex}");
                return ViewBagError("Erorr obteniendo usuario");
            }
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GetUsuarioModel usuario)
        {
            try
            {
                var res = await SendPostRequestAsync("Usuario/crear", usuario);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError($"UsuariosController.Create: {ex}");
                return ViewBagError("Error creando el usuario");
            }
        }

        // GET: UsuariosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await SendGetRequestAsync($"Usuario/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetUsuarioModel>());
            }
            catch(Exception ex)
            {
                _logger.LogError($"UsuariosController.Edit: {ex}");
                return ViewBagError("Error Obteniendo el usuario");
            }
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetUsuarioModel usuario)
        {
            try
            {
                var res = await SendPatchRequestAsync("Usuario/actualizar", usuario);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError($"UsuariosController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: UsuariosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await SendGetRequestAsync($"Usuario/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetUsuarioModel>());
            }
            catch(Exception ex)
            {
                _logger.LogError($"UsuariosController.Delete: {ex}");
                return ViewBagError("Error obteniendo el usuario");
            }
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, EliminarUsuarioModel collection)
        {
            try
            {
                var res = await SendDeleteRequestAsync("Usuario/eliminar", collection);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"UsuariosController.Delete: {ex}");
                return ViewBagError("Error eliminando usuario");
            }
        }
    }
}