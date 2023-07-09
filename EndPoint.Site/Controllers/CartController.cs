using EndPoint.Site.Models;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Store.Application.Interfaces.FacadPatternSite;
using Store.Application.Services.Carts;
using Store.Application.Services.Users.Queries.GetUsers;
using Store.Common.ResultDto;
using Store.Domain.Entities.Users;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;
        private readonly IPostFacadSite _postFacadSite;
        private readonly CookiesManager cookiemanager;
        private readonly SignInManager<User> _signInManager;
        private readonly IGetUserDetailServices _getUserDetailServices;
        public CartController(ICartServices cartServices,
            IPostFacadSite postFacadSite,
            IGetUserDetailServices getUserDetailServices,
            SignInManager<User> signInManager)
        {
            _cartServices= cartServices;
            cookiemanager=new CookiesManager();
            _signInManager=signInManager;
            _getUserDetailServices=getUserDetailServices;
            _postFacadSite=postFacadSite;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Checkout() {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("/Authentication/SignIn");
            }
            var userId = ClaimUtility.GetUserId(User);
            var result = await _getUserDetailServices.Execute(userId);

            CheckOutAddress address = new CheckOutAddress()
            {
                Email=result.Data.Email,
                FullName=result.Data.Fullname,
                Mobile=result.Data.Mobile,
            };

            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, int? count)
        {
            var result =await _cartServices.AddToCart(productId, cookiemanager.GetBrowserId(HttpContext), count);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartList()
        {

            var userId = ClaimUtility.GetUserId(User);
            var result = _cartServices.GetMyCart(cookiemanager.GetBrowserId(HttpContext), userId);
            return Json(new ResultDto{ IsSuccess=true,});
        }
        [HttpPost]
        public async Task<IActionResult> Remove(string Id)
        {
            return Json(await _cartServices.Remove(Id));
        }
        public IActionResult CartViewComponent()
        {
            return ViewComponent("Cart");
        }

        public IActionResult BacketViewComponent()
        {
            return ViewComponent("Backet");
        }
        public IActionResult CartTableViewComponent()
        {
            return ViewComponent("CartTable");
        }
        public IActionResult provinceViewComponent()
        {
            return ViewComponent("Province");
        }
        public IActionResult CityViewComponent(string provinceId)
        {
            return ViewComponent("City", provinceId);
        }
        public IActionResult BillsViewComponent(string cityId)
        {
            return ViewComponent("Bills", cityId);
        }

        

    }
}
