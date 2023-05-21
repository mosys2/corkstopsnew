using Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Contacts
{
    public class ContactType:BaseEntity
    {
        public string? Title { get; set; }
        public string? Value { get; set; }
        public string? Icon { get; set; }
        public string? CssClass { get; set; }
        public virtual ICollection<Contact> Contacts { get; set;}
    }
}
