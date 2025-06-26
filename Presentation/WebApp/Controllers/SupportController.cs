using Application.Dtos.SupportDtos;
using Application.Usecasses.CartItemServices;
using Application.Usecasses.CategoryServices;
using Application.Usecasses.SupportServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class SupportController : Controller
    {
        private readonly ICartItemServices _cartItemServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ISupportServices _supportServices;
        public SupportController(ICartItemServices cartItemServices, ICategoryServices categoryServices, ISupportServices supportServices)
        {
            _cartItemServices = cartItemServices;
            _categoryServices = categoryServices;
            _supportServices = supportServices;
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
        public async Task<IActionResult> Create(CreateSupportDto model)
        {
            model.CreatedDate = DateTime.Now;
            model.Status = 1;
            await _supportServices.CreateSupportAsync(model);
            return RedirectToAction("Index","Home");
        }
    }
}
