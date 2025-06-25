using Application.Usecasses.CartItemServices;
using Application.Usecasses.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class SupportController : Controller
    {
        private readonly ICartItemServices _cartItemServices;
        private readonly ICategoryServices _categoryServices;
        public SupportController(ICartItemServices cartItemServices, ICategoryServices categoryServices)
        {
            _cartItemServices = cartItemServices;
            _categoryServices = categoryServices;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
            ViewBag.CartItemCount = cartItemCount;

            return View();
        }
    }
}
