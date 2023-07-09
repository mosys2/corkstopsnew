using Microsoft.EntityFrameworkCore.Metadata;
using Store.Domain.Entities.Commons;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    public class Product:BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double LastPrice { get; set; }
        public int Quantity { get; set; }
        public string? Slug { get; set; }
        public bool IsActive { get; set; }
        public string? MinPic { get; set; }
        public string? Pic { get; set; }
        public double PostageFee { get; set; }
        public double PostageFeeBasedQuantity { get; set; }
        public string? Content { get; set; }

        //Relation To Category
        public virtual Category Category { get; set; }
        public string? CategoryId { get; set; }

        public virtual Brand Brand { get; set; }
        public string? BrandId { get; set; }

        public virtual User User { get; set; }
        public string? UserId { get; set; }

        
        public virtual ICollection<ItemTag> ItemTags { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Media> Medias { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
