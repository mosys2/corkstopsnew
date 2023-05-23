using Store.Application.Interfaces.Contexts;
using Store.Common.Constant.Enum;
using Store.Common.Constant;
using Store.Common.ResultDto;
using Store.Domain.Entities.Contacts;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Application.Services.Users.Commands.RegisterUser;

namespace Store.Application.Services.Users.Commands.Website.RegisterUser
{
    public interface IRegisterUser_Website
    {
        Task<ResultDto<ResultRegisterUserDto_Website>> Execute(RequestRegisterUserDto_Website request);
    }
    public class RegisterUser_Website : IRegisterUser_Website
    {
        private readonly IDataBaseContext _context;
        public RegisterUser_Website(IDataBaseContext context)
        {
            _context=context;
        }

        public async Task<ResultDto<ResultRegisterUserDto_Website>> Execute(RequestRegisterUserDto_Website request)
        {
            try
            {
                User user = new User()
                {
                    FullName=request.FullName,
                    InsertTime=DateTime.Now,
                    IsRemoved=false,
                    IsActive=true
                    
                };
                await _context.Users.AddAsync(user);

                Login login = new Login()
                {
                    User=user,
                    UserId=user.Id,
                    UserName="",
                    Password=request.Password,
                    InsertTime=DateTime.Now,
                    IsRemoved=false,
                };
                await _context.Logins.AddAsync(login);

                List<UserInRoll> userInRoll = new List<UserInRoll>();
                userInRoll.Add(new UserInRoll
                {
                    User=user,
                    UserId=user.Id,
                    RollId=(long)RollEnum.Customer,
                    InsertTime=DateTime.Now,
                    IsRemoved=false
                });
                await _context.UserInRolls.AddRangeAsync(userInRoll);

                List<Contact> contacts = new List<Contact>();
                contacts.Add(new Contact()
                {
                    User=user,
                    UserId=user.Id,
                    ContactTypeId=(long)ContactTypeEnum.Mobile,
                    Title=ContactTypeTitle.Mobile,
                    Value=request.Mobile,
                    InsertTime=DateTime.Now
                });
                contacts.Add(new Contact()
                {
                    User=user,
                    UserId=user.Id,
                    ContactTypeId=(long)ContactTypeEnum.Email,
                    Title=ContactTypeTitle.Email,
                    Value=request.Email,
                    InsertTime=DateTime.Now
                });
                await _context.Contacts.AddRangeAsync(contacts);

                //save change
                await _context.SaveChangesAsync();

                return new ResultDto<ResultRegisterUserDto_Website>
                {
                    Data=new ResultRegisterUserDto_Website
                    {
                        UserId=user.Id
                    },
                    IsSuccess=true,
                    Message="successs"
                };
            }catch(Exception ex)
            {
                return new ResultDto<ResultRegisterUserDto_Website>
                {
                    Data=new ResultRegisterUserDto_Website
                    {
                        UserId=0
                    },
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }
    }
    public class RequestRegisterUserDto_Website
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class ResultRegisterUserDto_Website
    {
        public long UserId { get; set; }
    }
}
