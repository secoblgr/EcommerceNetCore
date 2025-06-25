using Application.Usecasses.CartItemServices;
using Application.Usecasses.CategoryServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ICartItemServices _cartItemServices;
        public ContactController(ICategoryServices categoryServices, ICartItemServices cartItemServices)
        {
            _categoryServices = categoryServices;
            _cartItemServices = cartItemServices;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
            ViewBag.CartItemCount = cartItemCount;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }
    }
}
