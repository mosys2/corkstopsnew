﻿using Store.Domain.Entities.Commons;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    public class Rate:BaseEntity
    {
        public virtual Product Product  { get; set; }  
        public string? ProductId { get; set; }

        public virtual User User { get; set; }
        public string? UserId { get; set; }

        public int RateUser { get; set; }
    }
}
