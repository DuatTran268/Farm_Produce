using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
    public class UpdateUserAndOrdersDTO
    {
        public DetailUserDTO UserDTO { get; set; }
        public List<OrderDTO> OrderDTOs { get; set; }
    }
}
