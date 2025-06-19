using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AdressController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        

        public async Task <IActionResult> GetCity ()
        {
            return Json(new { success = true });
        }
    }

}
