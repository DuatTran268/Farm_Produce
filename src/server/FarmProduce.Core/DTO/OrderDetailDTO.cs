using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatusName { get; set; }
        public string PaymentMethodName { get; set; }
        // public int DiscountId { get; set; }
        public string CodeNameDiscount { get; set; }
        public IList<OrderItemDetailDTO> OrderItems { get; set; }
    }
}
