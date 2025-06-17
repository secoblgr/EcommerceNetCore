using Application.Usecasses.CategoryServices;
using Application.Usecasses.ProductServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IProductServices _productServices;
        public ProductController(ICategoryServices categoryServices, IProductServices productServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _productServices.GetByProductCategory(2);
            ViewBag.Categories = await _categoryServices.GetAllCategoryAsync();
            return View(values);
        }


    }
}
