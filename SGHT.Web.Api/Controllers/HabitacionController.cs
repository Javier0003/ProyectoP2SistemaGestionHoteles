using Microsoft.AspNetCore.Mvc;
using SGHT.Model.Model.habitacion;

namespace SGHT.Web.Api.Controllers
{
    public class HabitacionController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<GetHabitacionModel> habitacion = new List<GetHabitacionModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync("Habitacion");
                if (response.IsSuccessStatusCode)
                    habitacion = await response.Content.ReadFromJsonAsync<List<GetHabitacionModel>>();
                else
                {
                    ViewBag.Message = "Error obteniendo las habitaciones.";
                    return View();
                }
            }
            return View(habitacion);
        }

        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync($"Habitacion/{id}");
                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetHabitacionModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo las habitaciones.";
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
        public async Task<IActionResult> Create(CreateHabitacionModel habitacion)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");
                    var response = await client.PostAsJsonAsync("Habitacion/crear", habitacion);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error creando la habitacion.";
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
            GetHabitacionModel habitacion;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync($"Habitacion/{id}");
                if (response.IsSuccessStatusCode)
                    habitacion = await response.Content.ReadFromJsonAsync<GetHabitacionModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo la habitacion.";
                    return View();
                }
            }
            return View(habitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetHabitacionModel habitacion)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");
                    var response = await client.PatchAsJsonAsync("habitacion/actualizar", habitacion);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error actualizando la habitacion.";
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
            GetHabitacionModel habitacion;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync($"Categoria/{id}");
                if (response.IsSuccessStatusCode)
                    habitacion = await response.Content.ReadFromJsonAsync<GetHabitacionModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo la habitacion.";
                    return View();
                }
            }
            return View(habitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteHabitacionModel collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");
                    var request = new HttpRequestMessage(HttpMethod.Delete, "habitacion/eliminar")
                    {
                        Content = JsonContent.Create(collection)
                    };
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error eliminando la habitacion.";
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
