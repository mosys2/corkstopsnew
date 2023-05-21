using Store.Domain.Entities.Commons;
using Store.Domain.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Users
{
    public class User:BaseEntity
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int? Gender { get; set; }
        public string? Avatar { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<UserInRoll> UserInRolls { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }


    }
}
