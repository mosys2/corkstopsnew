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
        Task<ResultDto> Execute(long userId);
    }
    public class RemoveUserServices_Admin : IRemoveUserServices_Admin
    {
        private readonly IDataBaseContext _context;
        public RemoveUserServices_Admin(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Execute(long userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<ResultDto> Execute(long userId)
        //{
        //    var user =await _context.Users.FindAsync(userId);
        //    if (user==null)
        //    {
        //        return new ResultDto()
        //        {
        //            IsSuccess = false,
        //            Message = "کاربر یافت نشد",
        //        };
        //    }
        //    user.IsRemoved = true;
        //    user.RemoveTime = DateTime.Now;
        //    await _context.SaveChangesAsync();
        //    return new ResultDto
        //    {
        //        IsSuccess = true,
        //        Message = "کاربر با موفقیت حذف شد"
        //    };
        //}


    }
}
