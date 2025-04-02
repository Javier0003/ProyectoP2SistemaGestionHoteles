using Microsoft.AspNetCore.Mvc;
using SGHT.Model.Model.servicio;

namespace SGHT.Web.Api.Controllers
{
    public class ServicioController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<GetServicioModel> servicio = new List<GetServicioModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync("Servicios");
                if (response.IsSuccessStatusCode)
                    servicio = await response.Content.ReadFromJsonAsync<List<GetServicioModel>>();
                else
                {
                    ViewBag.Message = "Error obteniendo el servicio.";
                    return View();
                }
            }
            return View(servicio);
        }

        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync($"Servicio/{id}");
                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetServicioModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo el servicio.";
                    return View();
                }
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServicioModel servicio)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");
                    var response = await client.PostAsJsonAsync("Servicio/crear", servicio);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error creando el servicio.";
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            GetServicioModel servicio;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync($"Servicio/{id}");
                if (response.IsSuccessStatusCode)
                    servicio = await response.Content.ReadFromJsonAsync<GetServicioModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo el servicio.";
                    return View();
                }
            }
            return View(servicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetServicioModel servicio)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");
                    var response = await client.PatchAsJsonAsync("servicio/actualizar", servicio);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error actualizando el; servicio.";
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            GetServicioModel servicio;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync($"Servicio/{id}");
                if (response.IsSuccessStatusCode)
                    servicio = await response.Content.ReadFromJsonAsync<GetServicioModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo el servicio.";
                    return View();
                }
            }
            return View(servicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteServicioModel collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");
                    var request = new HttpRequestMessage(HttpMethod.Delete, "Servicio/eliminar")
                    {
                        Content = JsonContent.Create(collection)
                    };
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error eliminando el servicio.";
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
