using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppPurchases.Api.Controllers
{
    [ApiController]
    public class AuthenticateController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NonAction]
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
    }
}
