using Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    public class Category:BaseEntity
    {
        public virtual Category Parrent { get; set; }
        public string? ParrentId { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? CssClass { get; set; }
        public bool IsActive { get; set; }
        public int Sort { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }   
    }
}
