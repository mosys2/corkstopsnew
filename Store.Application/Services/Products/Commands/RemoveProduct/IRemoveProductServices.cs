using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.RemoveProduct
{
    public interface IRemoveProductServices
    {
        Task<ResultDto> Execute(string productId);
    }
    public class RemoveProductServices : IRemoveProductServices
    {
        private readonly IDataBaseContext _context;
        public RemoveProductServices(IDataBaseContext context)
        {
            _context=context;
        }
        public async Task<ResultDto> Execute(string productId)
        {
            try
            {
                var product = _context.Products.Find(productId);
                if (product == null) { return new ResultDto { IsSuccess=false, Message="Product not found" }; }

                //Tags
                var itemTags = await _context.ItemTags.Where(p => p.ProductId==product.Id).ToListAsync();
                if (itemTags.Any())
                {
                    _context.ItemTags.RemoveRange(itemTags);
                    await _context.SaveChangesAsync();
                }

                //Feater
                var features = await _context.Features.Where(p => p.ProductId==product.Id).ToListAsync();
                if (itemTags.Any())
                {
                    _context.Features.RemoveRange(features);
                    await _context.SaveChangesAsync();
                }

                //Media
                var medias = await _context.Medias.Where(p => p.ProductId==product.Id).ToListAsync();
                if (itemTags.Any())
                {
                    _context.Medias.RemoveRange(medias);
                    await _context.SaveChangesAsync();
                }

                product.IsRemoved= true;
                product.RemoveTime=DateTime.Now;
                await _context.SaveChangesAsync();
                return new ResultDto { IsSuccess=true, Message=Messages.Message.RemovedSuccess };
            }
            catch(Exception ex)
            {
                return new ResultDto { IsSuccess=false,Message=ex.Message};
            }
        }
    }
}
