using Application.Usecasses.CartItemServices;
using Application.Usecasses.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class FaqsController : Controller
    {
        private readonly ICartItemServices _cartItemServices;      
        private readonly ICategoryServices _categoryServices;

        public FaqsController(ICategoryServices categoryServices, ICartItemServices cartItemServices)
        {
            _categoryServices = categoryServices;
            _cartItemServices = cartItemServices;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
            ViewBag.CartItemCount = cartItemCount;

            return View();
        }
    }
}
