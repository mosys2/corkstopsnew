﻿using EndPoint.Site.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mst_Cms.Application.Services.Users.Command.LoginUser;
using Store.Application.Services.Users.Commands.Website.RegisterUser;
using Store.Common.Constant;
using Store.Common.ResultDto;
using System.Security.Claims;
using Store.Domain.Entities.Users;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRegisterUser_Website _registerUser;
        private readonly ILoginUserService _loginUserService;
        public AuthenticationController(IRegisterUser_Website registerUser,
            ILoginUserService loginUserService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _registerUser = registerUser;
            _loginUserService = loginUserService;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegistrUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto
                {
                    IsSuccess= false,
                    Message="Fields has Error!"
                });
            }
            var result = await _registerUser.Execute(new RequestRegisterUserDto_Website
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
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto
                {
                    IsSuccess= false,
                    Message=Messages.ErrorsMessage.ValidationError
                });
            }
            var login = await _loginUserService.Execute(model.Username, model.Password, false);
            return Json(new ResultDto
            {
                IsSuccess= login.IsSuccess,
                Message=login.Message,
            });


        }
    }
}

