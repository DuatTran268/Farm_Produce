using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.OrderStatuses;

namespace FarmProduct.WebApi.Models.Orders
{
    public class OrderEditModel
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public int DiscountId { get; set; }
        public int PaymentMethodId { get; set; }
        public string ApplicationUserId { get; set; }
        public IList<OrderItemDTO> OrderItems { get; set; }


        public static async ValueTask<OrderEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();

            var orderItemIds = form["OrderItemIds"].Select(id => int.Parse(id)).ToList();

            var orderEditModel = new OrderEditModel()
            {
                Id = int.Parse(form["Id"]),
                DiscountId = int.Parse(form["DiscountId"]),
                PaymentMethodId = int.Parse(form["PaymentMethodId"]),
                TotalPrice = int.Parse(form["TotalPrice"]),
                OrderStatusId = int.Parse(form["OrderStatusId"]),
                ApplicationUserId = form["ApplicationUserId"],
                OrderItems = new List<OrderItemDTO>()
            };

            foreach (var orderId in orderItemIds)
            {
                orderEditModel.OrderItems.Add(new OrderItemDTO { Id = orderId });
            }

            return orderEditModel;
        }

    }
}

