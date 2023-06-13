﻿using Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    public class Brand:BaseEntity
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Pic { get; set; }
        public string? CssClass { get; set; }

        public virtual ICollection<Product> Products { get;set; }
    }
}
