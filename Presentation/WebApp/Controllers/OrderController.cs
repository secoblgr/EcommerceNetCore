using Application.Dtos.OrderDtos;
using Application.Dtos.OrderItemDtos;
using Application.Interfaces.IOrdersRepository;
using Application.Usecasses.CartItemServices;
using Application.Usecasses.CartServices;
using Application.Usecasses.CategoryServices;
using Application.Usecasses.OrderServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ICartItemServices _cartItemServices;
        private readonly IOrderServices _orderServices;
        private readonly ICartServices _cartServices;
        public OrderController(ICartItemServices cartItemServices, ICategoryServices categoryServices, IOrderServices orderServices, ICartServices cartServices)
        {
            _cartItemServices = cartItemServices;
            _categoryServices = categoryServices;
            _orderServices = orderServices;
            _cartServices = cartServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CheckOut(int cartId)
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
            ViewBag.CartItemCount = cartItemCount;
            decimal shippingPrice = 9.90m;
            ViewBag.Shipping = shippingPrice;

            var values = await _cartServices.GetByIdCartAsync(cartId);
            if (values == null)
            {
                return View();
            }
            return View(values);

        }

        public async Task<IActionResult> GetCity()
        {
            var values = await _orderServices.GetAllCity();
            return Json(new { success = true, data = values });
        }

        public async Task<IActionResult> GetTownByCityId(int cityId)
        {
            var values = await _orderServices.GetTownByCityId(cityId);
            return Json(new { success = true, data = values });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto, int cartId)
        {
            decimal shippingPrice = 9.90m;
            ViewBag.Shipping = shippingPrice;
            try
            {
                var cart = await _cartServices.GetByIdCartAsync(cartId);
                List<CreateOrderItemDto> result = new();

                foreach (var item in cart.CartItems)
                {
                    var neworderItem = new CreateOrderItemDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.TotalPrice + shippingPrice,
                    };
                    result.Add(neworderItem);
                }
                dto.CustomerId = 1;                 //login kısmı eklenene kadar şimdilik statik id atadık.
                dto.OrderItems = result;
                dto.OrderStatus = "Completed";
                await _orderServices.CreateOrderAsync(dto);
                return Json(new { success = true });
            }
            catch (Exception err)
            {
                return Json(new {error = err});
            }
        }
    }
}

