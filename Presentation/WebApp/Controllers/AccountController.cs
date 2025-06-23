using Application.Usecasses.CartItemServices;
using Application.Usecasses.CategoryServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ICartItemServices _cartItemServices;

        public AccountController(ICategoryServices categoryServices, ICartItemServices cartItemServices)
        {
            _categoryServices = categoryServices;
            _cartItemServices = cartItemServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
            ViewBag.CartItemCount = cartItemCount;
            return View();
        }
        public async Task<IActionResult> Register()
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
            ViewBag.CartItemCount = cartItemCount;
            return View();
        }
    }
}
