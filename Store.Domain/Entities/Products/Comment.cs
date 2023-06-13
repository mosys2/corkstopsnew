using Store.Domain.Entities.Commons;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    public class Comment:BaseEntity
    {
        public virtual Comment Parrent { get; set; }
        public string? ParrentId { get; set; } 
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual  Product Product { get; set; }
        public string ProductId { get; set; }
        public bool Approved { get; set; }
        public virtual ICollection<Comment> SubComments { get; set; }

    }
}
