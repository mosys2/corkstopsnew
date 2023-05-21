using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
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
        public UserController(IGetUsersServices getUsers,
            IGetAllRolls_ForAdmin getAllRolls,
            IRegisterUser_Admin registerUser_Admin,
            IRemoveUserServices_Admin removeUserServices_Admin,
            IGetUserDetailServices getUserDetailServices,
            IEditeUserServicess editeUserServicess)
        {
            _getUsers=getUsers;
            _getAllRolls=getAllRolls;
            _registerUser_Admin=registerUser_Admin;
            _removeUserServices_Admin=removeUserServices_Admin;
            _getUserDetailServices=getUserDetailServices;
            _editeUserServicess=editeUserServicess;
        }

        public IActionResult Index(int Page=1,int PageSize=20)
        {
            var result = _getUsers.Execute(new RequestGetUserDto()
            {
                Page= Page,
                PageSize=PageSize,
                SerachKey=""
            });
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Rolls=new SelectList(_getAllRolls.Execute().Data, "Id", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Add([FromBody]UserModel_Request request)
        {
            if (ModelState.IsValid)
            {
              var result=_registerUser_Admin.Execute(new RequestRegisterUserDto
                {
                    Name=request.Name,
                    LastName=request.LastName,
                    Gender=request.Gender,
                    Rolls=request.Rolls,
                    IsActive=request.IsActive,
                    Email=request.Email,
                    Mobile=request.Mobile,
                    Username=request.Username,
                    Password=request.Password,
                    Address=request.Address,
                });
                if(result.IsSuccess)
                return Json(new ResultDto
                {
                    IsSuccess=true,
                    Message="success"
                });
            }
            return Json(new ResultDto
            {
                IsSuccess=false,
                Message="Error"
            });
        }

        [HttpPost]
        public IActionResult Delete([FromBody] DelleteUserModel_Request request)
        {
            return Json(_removeUserServices_Admin.Execute(request.currentItemId));
        }

        [HttpGet]
        public IActionResult Edite(long id)
        {
            ViewBag.Rolls=new SelectList(_getAllRolls.Execute().Data, "Id", "Title");
            var result =_getUserDetailServices.Execute(id).Data;
            UserModel_Edite user = new UserModel_Edite()
            {
                Id=result.Id,
                Name=result?.Name,
                LastName=result.LastName,
                Rolls=result.Rolls,
                Gender=result.Gender,
                IsActive=result.IsActive,
                Mobile=result.Mobile,
                Email=result.Email,
                Username=result.Username,
                Address=result.Address
            };
            return View(user);
        }

        [HttpPost]
        public IActionResult Edite([FromBody]UserModel_Edite request)
        {
            if (ModelState.IsValid)
            {
               var result= _editeUserServicess.Execute(new UserEditeDetailDto
               {
                   Id=request.Id,
                   Name=request.Name,
                   LastName=request.LastName,
                   Rolls=request.Rolls,
                   Gender=request.Gender,
                   IsActive=request.IsActive,
                   Mobile=request.Mobile,
                   Email=request.Email,
                   Username=request.Username,
                   Address=request.Address
               });
                return Json(new ResultDto
                {
                    IsSuccess=true,
                    Message=Messages.Message.RegisterSuccess
                });
            }
            return Json(new ResultDto
            {
                IsSuccess=false,
                Message=Messages.ErrorsMessage.RegisterFeaild
            });
        }
    }
    public class UserModel_Request
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public int[] Rolls { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByMobile", controller: "Common")]

        public string Mobile { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByEmail",controller:"Common")]
        public string Email { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByUsername", controller: "Common")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
    }
    public class DelleteUserModel_Request
    {
        public long currentItemId { get; set; }
    }
    public class UserModel_Edite
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public long[] Rolls { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByMobile", controller: "Common")]

        public string Mobile { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByEmail", controller: "Common")]

        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Remote(action: "CheckUserExistByUsername", controller: "Common")]

        public string Address { get; set; }
    }
}
