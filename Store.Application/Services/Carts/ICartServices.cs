using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.ResultDto;
using Store.Domain.Entities.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Carts
{
    public interface ICartServices
    {
        Task<ResultDto> AddToCart(Guid ProductId, Guid BrowserId, int? count);
        Task<ResultDto>RemoveFromCart(Guid ProductId, Guid BrowserId);
        Task<ResultDto<CartDto>> GetMyCart(Guid BrowserId, Guid? UserId, bool? Forpay = false);
        Task<ResultDto>AddCount(Guid Id);
        Task<ResultDto>MinCount(Guid Id);
        Task<ResultDto>Remove(Guid Id);
    }
    public class CartServices : ICartServices
    {
        private readonly IDataBaseContext _context;
        public CartServices(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> AddCount(Guid Id)
        {
            var result =await _context.CartItems.FindAsync(Id);
            if (result==null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message =Messages.ErrorsMessage.NotFound
                };

            }

            result.Count++;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = Messages.Message.AddSuccess
            };
        }

        public async Task<ResultDto> AddToCart(Guid ProductId, Guid BrowserId, int? count)
        {
            var cart =await _context.Carts
                  .Where(p => p.BrowserId == BrowserId && p.Finished == false)
                  .FirstOrDefaultAsync();
            if (cart == null)
            {
                Cart newCart = new Cart()
                {
                    Finished = false,
                    BrowserId = BrowserId,
                    InsertTime=DateTime.Now,


                };
                _context.Carts.Add(newCart);
                _context.SaveChanges();
                cart = newCart;
            }
            var product =await _context.Products.FindAsync(ProductId);
            var cartItem =await _context.CartItems
                .Where(p => p.ProductId == ProductId.ToString() && p.CartId == cart.Id)
                .FirstOrDefaultAsync();
            if (cartItem!=null)
            {
                cartItem.Count++;
            }
            else
            {
                CartItem newCartItem = new CartItem()
                {
                    Cart = cart,
                    Count = count.HasValue ? count.Value : 1,
                    Price = product.Price,
                    Product = product,
                    InsertTime=DateTime.Now,
                };
                _context.CartItems.Add(newCartItem);
            }
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message=$"Added to backet {product?.Name}",
            };
        }

        public async Task<ResultDto<CartDto>> GetMyCart(Guid BrowserId, Guid? UserId, bool? Forpay = false)
        {
            var cart =await _context.Carts
              .Include(p => p.CartItems)
              .ThenInclude(p => p.Product)
              .Where(p => p.BrowserId == BrowserId  && p.Finished == false)
              .OrderByDescending(p => p.Id)
              .FirstOrDefaultAsync();

            if (UserId != null&& cart!=null && Forpay==false)
            {
                var user =await _context.Users.FindAsync(UserId);
                cart.User = user;
                await _context.SaveChangesAsync();
            }
            if (cart != null)
            {
                return new ResultDto<CartDto>()
                {
                    Data = new CartDto()
                    {
                        productCount = cart.CartItems.Count(),
                        sumAmount = cart.CartItems.Sum(p => p.Price * p.Count),
                        CartId = cart.Id,
                        CartItems = cart.CartItems.Select(p => new CartItemDto
                        {
                            Id = p.Id,
                            ProductId = p.ProductId,
                            Count = p.Count,
                            Price = p.Price= p.Product.Price,
                            ProductName = p.Product.Name,
                            imageSrc = string.IsNullOrEmpty(p.Product.MinPic) ? PublicConst.NoImageUrl : PublicConst.FtpUrl+ p.Product.MinPic,
                        }).ToList(),
                    },
                    IsSuccess = true,
                };
            }
            else
            {
                return new ResultDto<CartDto>()
                {
                    Data=new CartDto()
                    {
                        productCount = 0,
                        sumAmount = 0,
                    },
                    IsSuccess = false,
                };
            }
        }

        public async Task<ResultDto> MinCount(Guid Id)
        {
            var result =await _context.CartItems.FindAsync(Id);
            if (result.Count<=1)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = Messages.ErrorsMessage.InvalidValue
                };
            }
            if (result == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = Messages.ErrorsMessage.NotFound
                };

            }

            result.Count--;
            await _context.SaveChangesAsync();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = Messages.Message.Decreased
            };
        }

        public async Task<ResultDto> Remove(Guid Id)
        {
            var result =await _context.CartItems.FindAsync(Id);
            if (result==null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message =Messages.ErrorsMessage.NotFound
                };
            }
            result.IsRemoved = true;
            result.RemoveTime = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = Messages.Message.RemovedSuccess
            };
        }

        public async Task<ResultDto> RemoveFromCart(Guid ProductId, Guid BrowserId)
        {
            var cartitem =await _context.CartItems.Where(p => p.Cart.BrowserId == BrowserId).FirstOrDefaultAsync();
            if (cartitem !=null)
            {
                cartitem.IsRemoved = true;
                cartitem.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message =Messages.Message.RegisterSuccess
                };
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message =Messages.ErrorsMessage.WrongRemove
                };
            }
        }
    }

    public class CartDto
    {
        public string CartId { get; set; }
        public int productCount { get; set; }
        public double sumAmount { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
    public class CartItemDto
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string imageSrc { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
