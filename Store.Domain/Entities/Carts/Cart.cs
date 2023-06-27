﻿using Store.Domain.Entities.Commons;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Carts
{
    public class Cart:BaseEntity
    {
        public User User { get; set; }
        public string? UserId { get; set; }
        public Guid BrowserId { get; set; }
        public bool Finished { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
    public class CartItem : BaseEntity
    {
        public virtual Product Product { get; set; }
        public string ProductId { get;set; }
        public int Count { get; set; }
        public double Price { get; set; }

        public virtual Cart Cart { get; set; }
        public string CartId { get; set; }
    }
}
