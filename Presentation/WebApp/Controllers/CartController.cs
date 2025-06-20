using Application.Dtos.CartItemDtos;
using Application.Usecasses.CartItemServices;
using Application.Usecasses.CartServices;
using Application.Usecasses.CategoryServices;
using Domain.Entities;
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
        public async Task<IActionResult> Index(int id)
        {
            var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
            ViewBag.CartItemCount = cartItemCount;
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;

            var value = await _cartServices.GetByIdCartAsync(id);
            return View(value);
        }

        //ajax ile verileri aldık arayüzden.
        [HttpPost]
        public async Task<JsonResult> AddToCartItem([FromBody] CreateCartItemDto model)
        {
            try
            {
                model.CartId = 2;
                var cart = await _cartServices.GetByIdCartAsync(model.CartId);
                var check = await _cartItemServices.CheckCartItem(model.CartId, model.ProductId);

                if (check)
                {
                    await _cartItemServices.UpdateQuantity(model.CartId, model.ProductId, model.Quantity);
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

        [HttpPost]
        public async Task<JsonResult> DeleteCartItem(int id)
        {
            try
            {
                if (id == 0)
                {
                    return Json(new { error = "Product not found !" });
                }
                var cartItem = await _cartItemServices.GetByIdCartItemAsync(id);
                if (cartItem == null)
                {
                    return Json(new { error = "Product not found !" });
                }

                await _cartItemServices.DeleteCartItemAsync(id);
                var cart = await _cartServices.GetByIdCartAsync(cartItem.CartId);          // carttaki ürünü sildikten sonra cartın fiyatını almak için cartı çagırıdk.
                var tempCartTotal = cart.TotalAmount - cartItem.TotalPrice;                // carttan silinen ürünün fiyatını sildik.
                await _cartServices.UpdateTotalAmount(cart.CartId, tempCartTotal);          // cart total amount update servisini çagırıp güncelledik.
                return Json(new { success = true });
            }
            catch (Exception err)
            {

                return Json(new { error = err });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(UpdateCartItemDto dto)
        {
            try
            {
                var cart = await _cartServices.GetByIdCartAsync(dto.CartId);
                // Güncelleme öncesi eski fiyatı al
                var oldCartItem = await _cartItemServices.GetByIdCartItemAsync(dto.CartItemId);
                decimal oldTotalPrice = oldCartItem.TotalPrice;
                // Miktarı güncelle
                await _cartItemServices.UpdateQuantity(dto.CartId, dto.ProductId, dto.Quantity);
                // Güncellenmiş haliyle item'ı tekrar çek
                var updatedCartItem = await _cartItemServices.GetByIdCartItemAsync(dto.CartItemId);
                decimal newTotalPrice = updatedCartItem.TotalPrice;
                // Farkı hesapla
                decimal priceDifference = newTotalPrice - oldTotalPrice;
                decimal newCartTotal = cart.TotalAmount + priceDifference;
                // Yeni toplamı güncelle
                await _cartServices.UpdateTotalAmount(cart.CartId, newCartTotal);
                return Json(new { success = true });
            }
            catch (Exception err)
            {
                return Json(new { success = false, error = err.Message });
            }
        }
    }
}
