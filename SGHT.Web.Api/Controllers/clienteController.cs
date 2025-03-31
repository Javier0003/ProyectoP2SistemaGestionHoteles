using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Base;
using SGHT.Model.Model.Cliente;
using SGHT.Model.Model.Recepcion;

namespace SGHT.Web.Api.Controllers
{
    public class clienteController : Controller
    {
        // GET: clienteController
        public async Task<IActionResult> Index()
        {
            List<GetClienteModel> clientes = new List<GetClienteModel>();
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync("Cliente");

                if (response.IsSuccessStatusCode)
                    clientes = await response.Content.ReadFromJsonAsync<List<GetClienteModel>>();
                else
                {
                    ViewBag.Message = "Error obteniendo los clientes.";
                    return View();
                }
            }

            return View(clientes);
        }

        // GET: clienteController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Cliente/{id}");

                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetClienteModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo el cliente.";
                    return View();
                }
            }
        }

        // GET: clienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: clienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgregarClienteModel cliente)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.PostAsJsonAsync("Cliente", cliente);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.Message = "Error creando el cliente.";
                    return View();
                }
            }
        }

        // GET: clienteController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Cliente/{id}");

                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetClienteModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo el cliente.";
                    return View();
                }
            }
        }

        // PUT: clienteController/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActualizarCliente cliente)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.PutAsJsonAsync($"Cliente/{id}", cliente);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.Message = "Error actualizando el cliente.";
                    return View();
                }
            }
        }

        // GET: RecepcionController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Cliente/{id}");

                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetClienteModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo el cliente.";
                    return View();
                }
            }
        }

        // POST: RecepcionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ActualizarCliente cliente)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var request = new HttpRequestMessage(HttpMethod.Delete, "Cliente/eliminar")
                    {
                        Content = JsonContent.Create(cliente)
                    };

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = "Error eliminando el cliente.";
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }
        }

    }
}
