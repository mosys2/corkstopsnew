using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using Store.Domain.Entities.Contacts;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Commands.RegisterUser
{
    public interface IRegisterUser_Admin
    {
       Task<ResultDto<ResultRegisterUserDto>> Execute(RequestRegisterUserDto request);
    }
    public class RegisterUser_Admin : IRegisterUser_Admin
    {
        private readonly IDataBaseContext _context;
        public RegisterUser_Admin(IDataBaseContext context)
        {
            _context=context;
        }

        public async Task<ResultDto<ResultRegisterUserDto>> Execute(RequestRegisterUserDto request)
        {
            User user = new User()
            {
                Name=request.Name,
                LastName=request.LastName,
                FullName=request.Name+" "+request.LastName,
                Gender=request.Gender,
                InsertTime=DateTime.Now,
                IsRemoved=false
            };
            Login login = new Login()
            {
                User=user,
                UserId=user.Id,
                UserName=request.Username,
                Password=request.Password,
                InsertTime=DateTime.Now,
                IsRemoved=false,
            };
            List<UserInRoll> userInRoll = new List<UserInRoll>();
            foreach(var item in request.Rolls)
            {
                userInRoll.Add(new UserInRoll
                {
                    User=user,
                    UserId=user.Id,
                    RollId=item,
                    InsertTime=DateTime.Now,
                    IsRemoved=false
                });
            }

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
            if(request.Address.Trim().Length > 0)
            {
                contacts.Add(new Contact()
                {
                    User=user,
                    UserId=user.Id,
                    ContactTypeId=(long)ContactTypeEnum.Address,
                    Title=ContactTypeTitle.Address,
                    Value=request.Address,
                    InsertTime=DateTime.Now
                });
            }
            //add & save change
           await _context.Users.AddAsync(user);
           await _context.Logins.AddAsync(login);
           await _context.UserInRolls.AddRangeAsync(userInRoll);
           await _context.Contacts.AddRangeAsync(contacts);
           await _context.SaveChangesAsync();

            return new ResultDto<ResultRegisterUserDto>
            {
                Data=new ResultRegisterUserDto
                {
                    UserId=user.Id
                },
                IsSuccess=true,
                Message="successs"
            };
        }
    }
    public class RequestRegisterUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int[] Rolls { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
    public class ResultRegisterUserDto
    {
        public long UserId { get; set; }
    }
}
