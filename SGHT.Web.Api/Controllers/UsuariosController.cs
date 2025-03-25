using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Web.Api.Controllers
{
    public class UsuariosController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuariosController> _logger;
        public UsuariosController(IConfiguration configuration, ILogger<UsuariosController> logger) : base(configuration) 
        {
            _configuration = configuration;
            _logger = logger;
        }


        // GET: UsuariosController
        public ActionResult Index()
        {
            var res = Get<dynamic>("Categoria/GetCategoria");
            return View();
        }

        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
