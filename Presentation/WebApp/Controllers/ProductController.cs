using Application.Interfaces.IProductsRepository;
using Application.Usecasses.CartItemServices;
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
        private readonly ICartItemServices _cartItemServices;
        public ProductController(ICategoryServices categoryServices, IProductServices productServices, ICartItemServices cartItemServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
            _cartItemServices = cartItemServices;
        }
        public async Task<IActionResult> Index(int categoryId, decimal min, decimal max, string sortOrder,string searchTerm)
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
            ViewBag.CartItemCount = cartItemCount;

            // Fiyat filtreleme
            ViewBag.Price_1_500 = (await _productServices.GetProductsByPrice(1, 500)).Count;
            ViewBag.Price_500_1000 = (await _productServices.GetProductsByPrice(500, 1000)).Count;
            ViewBag.Price_1000_2000 = (await _productServices.GetProductsByPrice(1000, 2000)).Count;
            ViewBag.Price_2000_5000 = (await _productServices.GetProductsByPrice(2000, 5000)).Count;

            // isime göre arama varsa 
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchedProducts = await _productServices.GetProductsSearch(searchTerm);
                return View(searchedProducts);
            }
            // Kategori filtrelemesi varsa
            if (categoryId != 0)
            {
                var categoryProducts = await _productServices.GetByProductCategory(categoryId);
                return View(categoryProducts);
            }

            // Fiyat aralığı filtrelemesi varsa
            if (min != 0 && max != 0)
            {
                var filteredProducts = await _productServices.GetProductsByPrice(min, max);
                return View(filteredProducts);
            }

            // Sıralama varsa
            if (!string.IsNullOrEmpty(sortOrder))
            {
                var sortedProducts = await _productServices.GetProductsSortedByPrice(sortOrder);
                return View(sortedProducts);
            }

            // Hiçbir filtre yoksa
            var allProducts = await _productServices.GetAllProductAsync();
            return View(allProducts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            var cartItemCount = await _cartItemServices.GetAllCartItemAsync();
            ViewBag.CartItemCount = cartItemCount;

            var product = await _productServices.GetByIdProductAsync(id);
            return View(product);
        }

    }
}
