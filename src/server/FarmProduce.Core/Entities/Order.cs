using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
    public class Order:IEntity
    {
        public int Id { get; set; }
        public DateTime DateOrder{ get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int PaymentMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public Discount Discount{ get; set; }

    }
}
