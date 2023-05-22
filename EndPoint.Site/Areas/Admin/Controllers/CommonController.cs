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

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckUserExistByEmail(string Email,long Id)
        {
            var user =await _checkUserExistByEmailServices.Excute(Email,Id);
            if (user==null || user.Count<1)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {Email} is already in use.");
            }
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckUserExistByMobile(string Mobile, long Id)
        {
            var user =await _checkUserExistByMobileServices.Excute(Mobile,Id);
            if (user==null || user.Count<1)
            {
                return Json(true);
            }
            else
            {
                return Json($"Mobile {Mobile} is already in use.");
            }
        }
        [AcceptVerbs("GET", "POST")]

        public async Task<IActionResult> CheckUserExistByUsername(string Username, long Id)
        {
            var user =await _checkUserExistByUsernameServices.Excute(Username,Id);
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
