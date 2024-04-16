using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.OrderStatuses;

namespace FarmProduct.WebApi.Models.Orders
{
    public class OrderEditModel
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public int TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public string ApplicationUserId { get; set; }

        public static async ValueTask<OrderEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();

            return new OrderEditModel()
            {
                Id = int.Parse(form["Id"]),
                TotalPrice = int.Parse(form["TotalPrice"]),

                OrderStatusId = int.Parse(form["OrderStatusId"]),
                ApplicationUserId = form["ApplicationUserId"],
            };
        }
    }
}
