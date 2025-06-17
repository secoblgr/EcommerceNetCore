using System.Diagnostics;
using System.Threading.Tasks;
using Application.Dtos.ProductDtos;
using Application.Usecasses.CategoryServices;
using Application.Usecasses.ProductServices;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly IProductServices _productServices;
    private readonly ICategoryServices _categoryServices;

    public HomeController(IProductServices productServices, ICategoryServices categoryServices)
    {
        _productServices = productServices;
        _categoryServices = categoryServices;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productServices.GetTakeAsync(12);
        var categories = await _categoryServices.GetAllCategoryAsync();
        ViewBag.Categories = categories;
       
        return View(products);
    }
}
