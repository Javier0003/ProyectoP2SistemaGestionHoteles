using Microsoft.AspNetCore.Mvc;
using SGHT.Model.Model.categoria;

namespace SGHT.Web.Api.Controllers
{
    public class CategoriaController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<GetCategoriaModel> categoria = new List<GetCategoriaModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync("Categoria");
                if (response.IsSuccessStatusCode)
                    categoria = await response.Content.ReadFromJsonAsync<List<GetCategoriaModel>>();
                else
                {
                    ViewBag.Message = "Error obteniendo las categorias.";
                    return View();
                }
            }
            return View(categoria);
        }

        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync($"Categoria/{id}");
                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetCategoriaModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo las categorias.";
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
        public async Task<IActionResult> Create(CreateCategoriaModel categoria)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");
                    var response = await client.PostAsJsonAsync("Categoria/crear", categoria);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error creando la categoria.";
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
            GetCategoriaModel categoria;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync($"Categoria/{id}");
                if (response.IsSuccessStatusCode)
                    categoria = await response.Content.ReadFromJsonAsync<GetCategoriaModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo las categorias.";
                    return View();
                }
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetCategoriaModel categoria)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");
                    var response = await client.PatchAsJsonAsync("categoria/actualizar", categoria);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error actualizando el las categorias.";
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
             GetCategoriaModel categoria;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");
                var response = await client.GetAsync($"Categoria/{id}");
                if (response.IsSuccessStatusCode)
                    categoria = await response.Content.ReadFromJsonAsync<GetCategoriaModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo las categorias.";
                    return View();
                }
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteCategoriaModel collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");
                    var request = new HttpRequestMessage(HttpMethod.Delete, "Categoria/eliminar")
                    {
                        Content = JsonContent.Create(collection)
                    };
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error eliminando la categoria.";
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