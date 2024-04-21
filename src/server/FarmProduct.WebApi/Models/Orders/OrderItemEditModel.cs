using FarmProduce.Core.Entities;

namespace FarmProduct.WebApi.Models.Orders
{
    public class OrderItemEditModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
      

        public static async Task<OrderItemEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();

            var orderItemEditModel = new OrderItemEditModel
            {
                Id = int.Parse(form["Id"]),
                OrderId = int.Parse(form["OrderId"]),
                ProductId = int.Parse(form["ProductId"]),
                Quantity = int.Parse(form["Quantity"]),
                          };

            return orderItemEditModel;
        }
    }
}