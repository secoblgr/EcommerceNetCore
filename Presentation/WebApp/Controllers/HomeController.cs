using System.Diagnostics;
using System.Threading.Tasks;
using Application.Dtos.ProductDtos;
using Application.Usecasses.CartItemServices;
using Application.Usecasses.CategoryServices;
using Application.Usecasses.ProductServices;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly IProductServices _productServices;
    private readonly ICategoryServices _categoryServices;
    private readonly ICartItemServices _cartItemServices;

    public HomeController(IProductServices productServices, ICategoryServices categoryServices, ICartItemServices cartItemServices)
    {
        _productServices = productServices;
        _categoryServices = categoryServices;
        _cartItemServices = cartItemServices;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productServices.GetTakeAsync(12);
        var categories = await _categoryServices.GetAllCategoryAsync();
        ViewBag.Categories = categories;
        var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
        ViewBag.CartItemCount = cartItemCount;

        return View(products);
    }
}
