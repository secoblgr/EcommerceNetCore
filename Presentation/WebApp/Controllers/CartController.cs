using Application.Dtos.CartItemDtos;
using Application.Usecasses.CartItemServices;
using Application.Usecasses.CartServices;
using Application.Usecasses.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ICartServices _cartServices;
        private readonly ICartItemServices _cartItemServices;
        public CartController(ICategoryServices categoryServices, ICartServices cartServices, ICartItemServices cartItemServices)
        {
            _categoryServices = categoryServices;
            _cartServices = cartServices;
            _cartItemServices = cartItemServices;
        }
        public async Task<IActionResult> Index(int id = 1)
        {
            decimal shippingPrice = 9.90m;
            ViewBag.Shipping = shippingPrice;
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;

            var value = await _cartServices.GetByIdCartAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<JsonResult> AddToCartItem([FromBody] CreateCartItemDto model)
        {
            try
            {
                model.CartId = 1;
                var cart = await _cartServices.GetByIdCartAsync(model.CartId);
                var check = await _cartItemServices.CheckCartItem(model.CartId, model.ProductId);

                if (check)
                {
                    await _cartItemServices.UpdateQuantity(model.CartId,model.ProductId,model.Quantity);  
                }
                else
                {
                    await _cartItemServices.CreateCartItemAsync(model);
                }

                var total = cart.TotalAmount = cart.TotalAmount + model.TotalPrice;
                await _cartServices.UpdateTotalAmount(model.CartId, total);

                return Json(new { success = true });
            }
            catch (Exception err)
            {
                return Json(new { error = err });
            }
        }
    }
}
