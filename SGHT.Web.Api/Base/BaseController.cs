using Microsoft.AspNetCore.Mvc;

namespace SGHT.Web.Api.Base
{
    public class BaseController : Controller
    {
        public IActionResult ViewBagError(string error)
        {
            ViewBag.Message = error;
            return View();
        }
    }
}
