using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Common;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        private readonly ICheckUserExistByEmailServices _checkUserExistByEmailServices;
        private readonly ICheckUserExistByMobileServices _checkUserExistByMobileServices;
        private readonly ICheckUserExistByUsernameServices _checkUserExistByUsernameServices;
        public CommonController(ICheckUserExistByEmailServices checkUserExistByEmailServices,
            ICheckUserExistByMobileServices checkUserExistByMobileServices,
            ICheckUserExistByUsernameServices checkUserExistByUsernameServices)
        {
            _checkUserExistByEmailServices=checkUserExistByEmailServices;
            _checkUserExistByMobileServices=checkUserExistByMobileServices;
            _checkUserExistByUsernameServices=checkUserExistByUsernameServices;
        }

        public IActionResult CheckUserExistByEmail(string Email)
        {
            var user = _checkUserExistByEmailServices.Excute(Email);
            if (user==null || user.Count<1)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {Email} is already in use.");
            }
        }
        public IActionResult CheckUserExistByMobile(string Mobile)
        {
            var user = _checkUserExistByMobileServices.Excute(Mobile);
            if (user==null || user.Count<1)
            {
                return Json(true);
            }
            else
            {
                return Json($"Mobile {Mobile} is already in use.");
            }
        }
        public IActionResult CheckUserExistByUsername(string Username)
        {
            var user = _checkUserExistByUsernameServices.Excute(Username);
            if (user==null || user.Count<1)
            {
                return Json(true);
            }
            else
            {
                return Json($"Username {Username} is already in use.");
            }
        }
    }
}
