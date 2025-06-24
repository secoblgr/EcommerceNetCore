using Application.Dtos.AccountDtos;
using Application.Usecasses.AccountServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var value = await _accountService.Register(dto);

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var value = await _accountService.Login(dto);

            if (value.Contains("Succesfully"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.value = value;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index","Home");
        }
    }
}
