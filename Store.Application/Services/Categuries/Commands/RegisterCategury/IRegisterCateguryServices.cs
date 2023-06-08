using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.ResultDto;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Categuries.Commands.RegisterCategury
{
    public interface IRegisterCategoryServices
    {
        Task<ResultDto> Execute(RequestRegisterCateguryDto request);
    }
    public class RegisterCategoryServices : IRegisterCategoryServices
    {
        private readonly IDataBaseContext _context;
        public RegisterCategoryServices(IDataBaseContext context)
        {
            _context=context;
        }
        public async Task<ResultDto> Execute(RequestRegisterCateguryDto request)
        {
            try
            {
                Category category = new Category
                {
                    Id=Guid.NewGuid().ToString(),
                    Name=request.Name,
                    ParrentId=request.ParrentCategoryId,
                    IsActive=request.IsActive,
                    Description=request.Description,
                    InsertTime=DateTime.Now,
                };
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                return new ResultDto()
                {
                    IsSuccess=true,
                    Message=Messages.Message.RegisterSuccess
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }


        }
    }
    public class RequestRegisterCateguryDto
    {
        public string Name { get; set; }
        public string ParrentCategoryId { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
