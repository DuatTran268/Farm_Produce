using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
    public class OrderUpdateDTO
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentMethodName { get; set; }
        // public int DiscountId { get; set; }
        public string CodeNameDiscount { get; set; }
        public IList<OrderItemDetailDTO> OrderItems { get; set; }
    }
}
