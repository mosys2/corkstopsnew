using Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Users
{
    public class Roll:BaseEntity
    {
        public string? Title { get; set; }
        public string? RollName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<UserInRoll>UserInRolls { get; set; }

    }
}
