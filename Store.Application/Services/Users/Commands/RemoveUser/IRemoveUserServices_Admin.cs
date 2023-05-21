using Store.Application.Interfaces.Contexts;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Commands.RemoveUser
{
    public interface IRemoveUserServices_Admin
    {
        public ResultDto Execute(long userId);
    }
    public class RemoveUserServices_Admin : IRemoveUserServices_Admin
    {
        private readonly IDataBaseContext _context;
        public RemoveUserServices_Admin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long userId)
        {
            var user = _context.Users.Find(userId);
            if (user==null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                };
            }
            user.IsRemoved = true;
            user.RemoveTime = DateTime.Now;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت حذف شد"
            };
        }


    }
}
