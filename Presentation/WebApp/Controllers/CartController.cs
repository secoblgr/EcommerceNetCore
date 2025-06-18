using Application.Usecasses.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CartController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;

            return View();
        }
    }
}
