using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Base;
using SGHT.Model.Model.rolUsuario;

namespace SGHT.Web.Api.Controllers
{
    public class RolUsuarioController : Controller
    {
        // GET: RolUsuarioController
        public async Task<IActionResult> Index()
        {
            List<GetRolUsuarioModel> roles = new List<GetRolUsuarioModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync("RolUsuario");

                if (response.IsSuccessStatusCode)
                    roles = await response.Content.ReadFromJsonAsync<List<GetRolUsuarioModel>>();
                else
                {
                    ViewBag.Message = "Error obteniendo los Roles.";
                    return View();
                }
            }

            return View(roles);
        }

        // GET: RolUsuarioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"RolUsuario/{id}");

                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetRolUsuarioModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo el rol.";
                    return View();
                }
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
                OperationResult result;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var response = await client.PostAsJsonAsync<CreateRolUsuarioModel>("RolUsuario/crear", rol);

                    if (response.IsSuccessStatusCode)
                        result = await response.Content.ReadFromJsonAsync<OperationResult>();
                    else
                    {
                        ViewBag.Message = "Error creando el rol.";
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: RolUsuarioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetRolUsuarioModel rol;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"RolUsuario/{id}");

                if (response.IsSuccessStatusCode)
                    rol = await response.Content.ReadFromJsonAsync<GetRolUsuarioModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo el rol.";
                    return View();
                }
            }

            return View(rol);
        }

        // POST: RolUsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetRolUsuarioModel rol)
        {
            OperationResult result;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var response = await client.PatchAsJsonAsync<GetRolUsuarioModel>("RolUsuario/actualizar", rol);

                    if (response.IsSuccessStatusCode)
                        result = await response.Content.ReadFromJsonAsync<OperationResult>();
                    else
                    {
                        ViewBag.Message = "Error actualizando el rol.";
                        return View();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: RolUsuarioController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            GetRolUsuarioModel rol;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"RolUsuario/{id}");

                if (response.IsSuccessStatusCode)
                    rol = await response.Content.ReadFromJsonAsync<GetRolUsuarioModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo el rol.";
                    return View();
                }
            }

            return View(rol);
        }

        // POST: RolUsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteRolUsuarioModel collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var request = new HttpRequestMessage(HttpMethod.Delete, "RolUsuario/eliminar")
                    {
                        Content = JsonContent.Create(collection)
                    };

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = "Error eliminando el rol.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
