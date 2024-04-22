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
    


        public static async Task<OrderEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();

       

            var orderEditModel = new OrderEditModel()
            {
                Id = int.Parse(form["Id"]),
                DiscountId = int.Parse(form["DiscountId"]),
                PaymentMethodId = int.Parse(form["PaymentMethodId"]),
                TotalPrice = int.Parse(form["TotalPrice"]),
                OrderStatusId = int.Parse(form["OrderStatusId"]),
                ApplicationUserId = form["ApplicationUserId"],
              
              
            };
            return orderEditModel;
           
        }

    }
}

