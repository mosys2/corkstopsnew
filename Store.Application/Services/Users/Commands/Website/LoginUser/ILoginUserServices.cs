using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using Store.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mst_Cms.Application.Services.Users.Command.LoginUser
{
    public interface ILoginUserService
    {
        Task<ResultDto<ResultUserloginDto>> Execute(string email, string password);
    }
    public class LoginUserService : ILoginUserService
    {
        private readonly IDataBaseContext _context;
        public LoginUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<ResultUserloginDto>> Execute(string email, string password)
        {
            throw new NotImplementedException();
        }
        //public async Task<ResultDto<ResultUserloginDto>> Execute(string EmailOrMobile, string password)
        //{
        //    var user = await _context.Contacts
        //         .Include(c => c.User)
        //         .ThenInclude(r => r.UserInRolls)
        //         .ThenInclude(r => r.Roll)
        //         .Where(p => p.Value==EmailOrMobile &&
        //         (p.ContactTypeId==(long)ContactTypeEnum.Email ||
        //         p.ContactTypeId==(long)ContactTypeEnum.Mobile))
        //         .FirstOrDefaultAsync();

        //    if (user==null)
        //    {
        //        return new ResultDto<ResultUserloginDto>()
        //        {
        //            IsSuccess = false,
        //            Message = "username or password is false!",
        //        };

        //    }
        //    if (user.User.IsActive!=true)
        //    {
        //        return new ResultDto<ResultUserloginDto>()
        //        {
        //            IsSuccess = false,
        //            Message = "Yor account is suspend!",
        //        };

        //    }
        //    var passwordhasher = new PasswordHasher();
        //    var signinUser =await _context.Logins.Where(s => s.UserId==user.UserId).FirstOrDefaultAsync();
        //    if (signinUser==null)
        //    {
        //        return new ResultDto<ResultUserloginDto>()
        //        {
        //            IsSuccess = false,
        //            Message = "dont register login for you!",
        //        };
        //    }
        //    bool resultverifypassword = passwordhasher.VerifyPassword(signinUser.Password, password);
        //    if (resultverifypassword==false)
        //    {
        //        return new ResultDto<ResultUserloginDto>()
        //        {

        //            IsSuccess = false,
        //            Message = "username or password is false!"
        //        };
        //    }


        //    //List<string> userRoles = _context.UserInRoles.Where(p => p.UserId == user.Id)
        //    // .ToList().Select(p => p.Role.Name).ToList();

        //    List<string> roles = new List<string>();
        //    foreach (var item in user.User.UserInRolls)
        //    {
        //        roles.Add(item.Roll.RollName);
        //    }
        //    return new ResultDto<ResultUserloginDto>()
        //    {
        //        Data = new ResultUserloginDto()
        //        {
        //            Roles = roles,
        //            UserId = user.User.Id,
        //            Name=user.User.FullName,
        //            Password=signinUser.Password,
        //        },
        //        IsSuccess=true,
        //        Message="you are logged in!"
        //    };


        //}
    }
    public class ResultUserloginDto
    {
        public long UserId { get; set; }
        public List<string> Roles { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

