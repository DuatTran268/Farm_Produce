using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
    public class OrderStatus:IEntity
    {
        public int Id { get; set; }
        
        public string StatusCode { get; set; }
        public DateTime StatusDate { get; set; }
        public string Description { get; set; } 
        public IList<Order> Order { get; set; }
    }
}
