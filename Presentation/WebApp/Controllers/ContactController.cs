using Application.Dtos.ContactDtos;
using Application.Usecasses.CartItemServices;
using Application.Usecasses.CategoryServices;
using Application.Usecasses.ContactServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ICartItemServices _cartItemServices;
        private readonly IContactServices _contactServices;
        public ContactController(ICategoryServices categoryServices, ICartItemServices cartItemServices, IContactServices contactServices)
        {
            _categoryServices = categoryServices;
            _cartItemServices = cartItemServices;
            _contactServices = contactServices;
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
        public async Task<IActionResult> Create(CreateContactDto model)
        {
            model.CreatedDate = DateTime.Now;
            model.Status = 1;
            await _contactServices.CreateContactAsync(model);
            return RedirectToAction("Index", "Contact");
        }
    }
}
