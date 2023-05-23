using Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Users
{
    public class Login:BaseEntity
    {
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }

       
    }
}
