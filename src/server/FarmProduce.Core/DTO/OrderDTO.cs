using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public int TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public string ApplicationUserId { get; set; }
        public IList<PaymentMethod> PaymentMethods { get; set; }
        public IList<OrderStatus> OrderStatuses { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }
}

