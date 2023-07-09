using Store.Common.Constant.Enum;
using Store.Domain.Entities.Commons;
using Store.Domain.Entities.Finances;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Store.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual RequestPay RequestPay { get; set; }
        public string RequestPayId { get; set; }
        public OrderState OrderState { get; set; }
        public string? TrackingPost { get; set; }
        public string FullName { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool Seen { get; set; } = false;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderDetail : BaseEntity
    {
        public virtual Order Order { get; set; }
        public string OrderId { get; set; }
        public virtual Product Product { get; set; }
        public string ProductId { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
    
}
