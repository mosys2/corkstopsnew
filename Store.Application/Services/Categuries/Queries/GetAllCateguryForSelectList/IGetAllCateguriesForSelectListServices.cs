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

            categury.Add(new AllCateguriesSelectListDto
            {
                Id=null,
                Name="Uncategorized",
                ParrentId=null
            });

            var result = await _context.Categories.Select(p => new AllCateguriesSelectListDto()
            {
                Id=p.Id,
                Name=p.Name,
                ParrentId=p.ParrentId,
                InsertTime=p.InsertTime
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
                    ParrentId=item.ParrentId,
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
                        ParrentId=itemchild.ParrentId
                    });
                    listGnrator(childN, level);
                }
                else
                {
                    categury.Add(new AllCateguriesSelectListDto
                    {
                        Id=itemchild.Id,
                        Name=DashGnrator(level)+" "+itemchild.Name,
                        ParrentId=itemchild.ParrentId
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
        public string? ParrentId { get; set; }
        public DateTime? InsertTime { get; set; }
    }
}
