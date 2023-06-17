using Store.Application.Interfaces.Contexts;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Queries.GetAllCategories
{
    public interface IGetAllCategoriesService
    {
        Task<List<AllCateguriesListDto>> Execute();
    }
    public class GetAllCategoriesService : IGetAllCategoriesService
    {
        private readonly IDataBaseContext _context;
        public static List<AllCateguriesListDto> categury = new List<AllCateguriesListDto>();
        public static List<AllCateguriesListDto> allCategury = new List<AllCateguriesListDto>();

        public GetAllCategoriesService(IDataBaseContext context)
        {
            _context=context;
        }
        public async Task<List<AllCateguriesListDto>> Execute()
        {
            categury.Clear();
            allCategury.Clear();

            var result = _context.Categories.Select(p => new AllCateguriesListDto()
            {
                Id=p.Id,
                Name=p.Name,
                ParrentId=p.ParrentId,
                InsertTime=p.InsertTime,
                IsActive=p.IsActive
            }).OrderBy(o => o.InsertTime).ToList();
            

            allCategury.AddRange(result);

            foreach (var item in result.Where(p => p.ParrentId==null))
            {
                int level = 1;
                categury.Add(new AllCateguriesListDto
                {
                    Id=item.Id,
                    Name=item.Name,
                    OrginalName=item.Name,
                    ParrentId=item.ParrentId,
                    ParrentName="Master",
                    IsActive=item.IsActive
                });

                var childe = result.Where(p => p.ParrentId==item.Id).ToList();
                listGnrator(childe, level);
            }
            return categury;
        }
        public void listGnrator(List<AllCateguriesListDto> selectListDtos, int level)
        {
            level++;
            foreach (var itemchild in selectListDtos)
            {
                var childN = allCategury.Where(p => p.ParrentId==itemchild.Id).ToList();
                if (childN.Any())
                {
                    categury.Add(new AllCateguriesListDto
                    {
                        Id=itemchild.Id,
                        Name=DashGnrator(level)+" "+itemchild.Name,
                        OrginalName=itemchild.Name,
                        ParrentId=itemchild.ParrentId,
                        IsActive=itemchild.IsActive,
                        Description=itemchild.Description,
                        ParrentName=allCategury.Where(c => c.Id==itemchild.ParrentId).FirstOrDefault()?.Name
                    });
                    listGnrator(childN, level);
                }
                else
                {
                    categury.Add(new AllCateguriesListDto
                    {
                        Id=itemchild.Id,
                        Name=DashGnrator(level)+" "+itemchild.Name,
                        ParrentId=itemchild.ParrentId,
                        OrginalName=itemchild.Name,
                        IsActive=itemchild.IsActive,
                        Description=itemchild.Description,
                        ParrentName=allCategury.Where(c => c.Id==itemchild.ParrentId).FirstOrDefault()?.Name
                    });
                }
            }
            return;
        }
        public string DashGnrator(int number)
        {
            string dash = "";
            string space = "";
            for (int i = 1; i<number; i++)
            {
                dash+=" - ";
                space+=" ";
            }
            return space+dash;
        }

    }
    public class AllCateguriesListDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? OrginalName { get; set; }
        public string? ParrentName { get; set; }
        public string? ParrentId { get; set; }
        public DateTime? InsertTime { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
    }
}
