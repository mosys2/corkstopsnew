using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Categuries.Queries.GetAllCateguryForSelectList;
using Store.Common.Constant;
using Store.Common.ResultDto;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Categuries.Commands.RemoveCategory
{
    public interface IRemoveCategoryServices
    {
        Task<ResultDto> Execute(string categoryId);
    }

    public class RemoveCategoryServices : IRemoveCategoryServices
    {
        private readonly IDataBaseContext _context;
        public static List<AllCategoriesSelectListDto> category = new List<AllCategoriesSelectListDto>();
        public static List<AllCategoriesSelectListDto> allCategory = new List<AllCategoriesSelectListDto>();

        public RemoveCategoryServices(IDataBaseContext context)
        {
            _context=context;
        }

        public async Task<ResultDto> Execute(string categoryId)
        {
            try
            {
                var result = await _context.Categories.Select(p => new AllCategoriesSelectListDto()
                {
                    Id=p.Id,
                    ParrentId=p.ParrentId
                }).ToListAsync();
                allCategory.AddRange(result);

                var findedCategory = allCategory.Where(p => p.Id==categoryId).FirstOrDefault();

                if (findedCategory==null) { return new ResultDto { IsSuccess=false, Message="Not found" }; }
                category.Add(new AllCategoriesSelectListDto
                {
                    Id=findedCategory.Id,
                });

                var childe = allCategory.Where(p => p.ParrentId==findedCategory.Id).ToList();
                if(childe.Any())
                {
                    listFinder(findedCategory.Id);
                }


                foreach (var item in category)
                {
                    var selectedItem = await _context.Categories.Where(p => p.Id==item.Id).FirstOrDefaultAsync();
                    if (selectedItem!=null)
                    {
                        selectedItem.IsRemoved=true;
                        selectedItem.RemoveTime=DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                }
                return new ResultDto
                {
                    IsSuccess=true,
                    Message=Messages.Message.RemovedSuccess
                };
            }catch(Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }
        public void listFinder(string parrentId)
        {
            var selectListDtos = allCategory.Where(p => p.ParrentId==parrentId).ToList();
            foreach (var itemchild in selectListDtos)
            {
                var childN = allCategory.Where(p => p.Id==itemchild.Id).ToList();
                if (childN.Any())
                {
                    category.Add(new AllCategoriesSelectListDto
                    {
                        Id=itemchild.Id,
                    });
                    listFinder(itemchild.Id);
                }
                else
                {
                    category.Add(new AllCategoriesSelectListDto
                    {
                        Id=itemchild.Id,
                    });
                }
            }
            return;
        }
    }
    public class AllCategoriesSelectListDto
    {
        public string? Id { get; set; }
        public string? ParrentId { get; set; }
    }

}
