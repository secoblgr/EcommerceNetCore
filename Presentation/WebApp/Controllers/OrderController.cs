using Application.Interfaces.IOrdersRepository;
using Application.Usecasses.CartItemServices;
using Application.Usecasses.CategoryServices;
using Application.Usecasses.OrderServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ICartItemServices _cartItemServices;
        private readonly IOrderServices _orderServices;
        public OrderController(ICartItemServices cartItemServices, ICategoryServices categoryServices, IOrderServices orderServices)
        {
            _cartItemServices = cartItemServices;
            _categoryServices = categoryServices;
            _orderServices = orderServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CheckOut()
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
            ViewBag.CartItemCount = cartItemCount;

            return View();
        }

        public async Task<IActionResult> GetCity()
        {
            var values = await _orderServices.GetAllCity();
            return Json(new { success = true, data = values });
        }

        public async Task <IActionResult> GetTownByCityId(int cityId)
        {
            var values = await _orderServices.GetTownByCityId(cityId);
            return Json(new { success = true, data = values });
        }
    }
}
