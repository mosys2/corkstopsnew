using Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Users
{
    public class UserInRoll:BaseEntity
    {
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public virtual Roll Roll { get; set; }
        public long RollId { get; set; }
    }
}
