﻿using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
    public class PaymentMethod:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
