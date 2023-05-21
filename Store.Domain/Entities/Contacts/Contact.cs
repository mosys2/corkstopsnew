using Store.Domain.Entities.Commons;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Contacts
{
    public class Contact:BaseEntity
    {
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public virtual ContactType ContactType { get; set; }
        public long ContactTypeId { get; set; }
        public string? Title { get; set; }
        public string? Value { get; set; }

    }
}
