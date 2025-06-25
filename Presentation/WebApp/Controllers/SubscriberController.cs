using Application.Dtos.SubscriberDtos;
using Application.Usecasses.SubscriberServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly ISubscriberServices _subscriberServices;

        public SubscriberController(ISubscriberServices subscriberServices)
        {
            _subscriberServices = subscriberServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberDto subscriber)
        {
            subscriber.SubscribeDate = DateTime.Now;
            await _subscriberServices.CreateSubscriberAsync(subscriber);
            return RedirectToAction("Index", "Home");
        }
    }
}
