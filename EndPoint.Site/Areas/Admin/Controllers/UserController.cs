using EndPoint.Site.Areas.Admin.Models.ContollerModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Store.Application.Services.FileManager.Queries.ListDirectories;
using Store.Application.Services.Users.Commands.EditeUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Commands.RemoveUser;
using Store.Application.Services.Users.Queries.GetAllRolls;
using Store.Application.Services.Users.Queries.GetUsers;
using Store.Common.Constant;
using Store.Common.ResultDto;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IGetUsersServices _getUsers;
        private readonly IGetAllRolls_ForAdmin _getAllRolls;
        private readonly IRegisterUser_Admin _registerUser_Admin;
        private readonly IRemoveUserServices_Admin _removeUserServices_Admin;
        private readonly IGetUserDetailServices _getUserDetailServices;
        private readonly IEditeUserServicess _editeUserServicess;
        private readonly IGetListDirectoryServices _getListDirectory;
        public UserController(IGetUsersServices getUsers,
            IGetAllRolls_ForAdmin getAllRolls,
            IRegisterUser_Admin registerUser_Admin,
            IRemoveUserServices_Admin removeUserServices_Admin,
            IGetUserDetailServices getUserDetailServices,
            IEditeUserServicess editeUserServicess,
            IGetListDirectoryServices getListDirectory)
        {
            _getUsers=getUsers;
            _getAllRolls=getAllRolls;
            _registerUser_Admin=registerUser_Admin;
            _removeUserServices_Admin=removeUserServices_Admin;
            _getUserDetailServices=getUserDetailServices;
            _editeUserServicess=editeUserServicess;
            _getListDirectory=getListDirectory; 
        }
        [HttpGet]
        public IActionResult Index(string SearchKey = "", int Page = 1, int PageSize = 20)
        {
            var a = _getListDirectory.Execut("uploads");
            var result = _getUsers.Execute(new RequestGetUserDto()
            {
                Page= Page,
                PageSize=PageSize,
                SerachKey=SearchKey
            });
            return View(result.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = await _getAllRolls.Execute();
            ViewBag.Role=new SelectList(result.Data, "Name", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(UserModel_Request model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto
                {
                    IsSuccess=false,
                    Message=Messages.ErrorsMessage.ModelInvalid
                });
            }
            var result = await _registerUser_Admin.Execute(new RequestRegisterUserDto
            {
                Name=model.Name,
                LastName=model.LastName,
                Gender=model.Gender,
                LockoutEnabled=model.LockoutEnabled,
                Email=model.Email,
                Mobile=model.Mobile,
                Password=model.Password,
                Role=model.Role
            });
            return Json(new ResultDto
            {
                IsSuccess=result.IsSuccess,
                Message=result.Message
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DelleteUserModel_Request model)
        {
            return Json(await _removeUserServices_Admin.Execute(model.Id));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var rolse = await _getAllRolls.Execute();
            ViewBag.Role=new SelectList(rolse.Data, "Name", "Name");
            var result = await _getUserDetailServices.Execute(Id);
            UserModel_Edit user = new UserModel_Edit()
            {
                Id=result.Data.Id,
                Name=result.Data.Name,
                LastName=result.Data.LastName,
                Role=result.Data.Rols,
                Gender=result.Data.Gender.Value,
                LockoutEnabled=!result.Data.LockoutEnabled,
                Mobile=result.Data.Mobile,
                Email=result.Data.Email,
            };
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModel_Edit request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto
                {
                    IsSuccess=false,
                    Message=Messages.ErrorsMessage.ModelInvalid
                });
            }
            var result = await _editeUserServicess.Execute(new UserEditeDetailDto
            {
                Id=request.Id,
                Name=request.Name,
                LastName=request.LastName,
                Rols=request.Role,
                Gender=request.Gender,
                LockoutEnabled=request.LockoutEnabled,
                Mobile=request.Mobile,
                Email=request.Email,
            });
            return Json(new ResultDto
            {
                IsSuccess=result.IsSuccess,
                Message=result.Message
            });
        }
    }
}
