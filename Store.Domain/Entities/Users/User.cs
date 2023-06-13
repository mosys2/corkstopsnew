using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities.Commons;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Users
{
    public class User:IdentityUser
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Gender { get; set; } = 0;
        public string? ProfileImage { get; set; }
        public bool IsActive { get; set; }

        //Base Entity
        public DateTime? InsertTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }


    }
}
