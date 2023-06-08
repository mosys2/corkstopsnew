using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Categuries.Queries.GetAllCateguryForSelectList
{
    public interface IGetAllCateguriesForSelectListServices
    {
        Task<List<AllCateguriesSelectListDto>> Execute();
    }
    public class GetAllCateguriesForSelectListServices : IGetAllCateguriesForSelectListServices
    {
        private readonly IDataBaseContext _context;
        public static List<AllCateguriesSelectListDto> categury = new List<AllCateguriesSelectListDto>();
        public static List<AllCateguriesSelectListDto> allCategury = new List<AllCateguriesSelectListDto>();

        public GetAllCateguriesForSelectListServices(IDataBaseContext context)
        {
            _context=context;
        }
        public async Task<List<AllCateguriesSelectListDto>> Execute()
        {
            categury.Clear();
            allCategury.Clear();

            var result = await _context.Categories.Select(p => new AllCateguriesSelectListDto()
            {
                Id=p.Id,
                Name=p.Name,
                ParrentId=p.ParrentId,
                InsertTime=p.InsertTime,
                IsActive=p.IsActive
            }).OrderBy(o =>o.InsertTime)
            .ToListAsync();

            allCategury.AddRange(result);

            foreach (var item in result.Where(p => p.ParrentId==null))
            {
                int level = 1;
                categury.Add(new AllCateguriesSelectListDto
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
        public void listGnrator(List<AllCateguriesSelectListDto> selectListDtos, int level)
        {
            level++;
            foreach (var itemchild in selectListDtos)
            {
                var childN = allCategury.Where(p => p.ParrentId==itemchild.Id).ToList();
                if (childN.Any())
                {
                    categury.Add(new AllCateguriesSelectListDto
                    {
                        Id=itemchild.Id,
                        Name=DashGnrator(level)+" "+itemchild.Name,
                        OrginalName=itemchild.Name,
                        ParrentId=itemchild.ParrentId,
                        IsActive=itemchild.IsActive,
                        Description=itemchild.Description,
                        ParrentName=allCategury.Where(c=>c.Id==itemchild.ParrentId).FirstOrDefault()?.Name
                    });
                    listGnrator(childN, level);
                }
                else
                {
                    categury.Add(new AllCateguriesSelectListDto
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
            for(int i = 1; i<number; i++)
            {
               dash+=" - ";
               space+=" ";
            }
            return space+dash;
        }

    }
    public class AllCateguriesSelectListDto
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
