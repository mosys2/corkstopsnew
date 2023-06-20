using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.ResultDto;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.AddNewTagServices
{
    public interface IAddNewTagServices
    {
        Task<ResultDto> Execute(RequestRegisterTags_Dto request);
    }
    public class AddNewTagServices : IAddNewTagServices
    {
        private readonly IDataBaseContext _context;
        public AddNewTagServices(IDataBaseContext context)
        {
            _context=context;
        }

        public async Task<ResultDto> Execute(RequestRegisterTags_Dto request)
        {
            try
            {
                Tag tag = new Tag()
                {
                    Id=Guid.NewGuid().ToString(),
                    InsertTime=DateTime.Now,
                    Name=request.Name,
                };
                await _context.Tags.AddAsync(tag);
                await _context.SaveChangesAsync();
                return new ResultDto()
                {
                    IsSuccess=true,
                    Message=Messages.Message.RegisterSuccess
                };
            }
            catch (Exception ex)
            {
                return new ResultDto()
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }
    }
    public class RequestRegisterTags_Dto
    {
        public string Name { get; set; }
    }
}
