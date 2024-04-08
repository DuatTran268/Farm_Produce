using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
    public class UserWithRolesDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public List<Order> Orders { get; set; } 
    }
}
