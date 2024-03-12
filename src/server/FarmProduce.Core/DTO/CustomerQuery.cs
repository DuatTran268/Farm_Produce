using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
    public class CustomerQuery
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
