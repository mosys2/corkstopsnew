using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;
using Store.Common.ResultDto;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;
        private readonly CookiesManager cookiemanager;

        public CartController(ICartServices cartServices)
        {
            _cartServices= cartServices;
            cookiemanager=new CookiesManager();
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, int? count)
        {
            var result = _cartServices.AddToCart(productId, cookiemanager.GetBrowserId(HttpContext), count);
            return Json(result);
        }
    }
}
