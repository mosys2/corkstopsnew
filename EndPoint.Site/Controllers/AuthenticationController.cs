using EndPoint.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Users.Commands.Website.RegisterUser;
using Store.Common.ResultDto;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRegisterUser_Website _registerUser;
        public AuthenticationController(IRegisterUser_Website registerUser)
        {
            _registerUser = registerUser;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegistrUserModel model)
        {
            if(!ModelState.IsValid)
            {
                return Json(new ResultDto
                {
                    IsSuccess= false,
                    Message="Fields has Error!"
                });
            }
            var result= await _registerUser.Execute(new RequestRegisterUserDto_Website
            {
                Email=model.Email,
                Password=model.Password,
                FullName=model.FullName,
                Mobile=model.Mobile,
            });
            return Json(new ResultDto
            {
                IsSuccess= result.IsSuccess,
                Message=result.Message,
            }); 
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
