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
        public decimal TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public string ApplicationUserId { get; set; }
        public int PaymentMethodId { get; set; }
        public int DiscountId { get; set; }
        public IList<OrderItemDTO> OrderItems { get; set; }
    }
}

