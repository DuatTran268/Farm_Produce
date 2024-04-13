﻿using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
    public class DetailUserDTO
    {
        public string Id {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
