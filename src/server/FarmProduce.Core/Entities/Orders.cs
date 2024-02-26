using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
    public class Orders:IEntity
    {
        public int Id { get; set; }
        public DateTime DateOrder{ get; set; }
        public int TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
    }
}
