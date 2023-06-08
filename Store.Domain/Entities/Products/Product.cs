using Store.Domain.Entities.Commons;
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

    }
}
