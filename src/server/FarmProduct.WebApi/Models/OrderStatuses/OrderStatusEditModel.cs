using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Products;

namespace FarmProduct.WebApi.Models.OrderStatuses
{
    public class OrderStatusEditModel
    {
        public int Id { get; set; }

        public string StatusCode { get; set; }
        //public DateTime StatusDate { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public static async ValueTask<OrderStatusEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
          
            return new OrderStatusEditModel()
            {
                Id = int.Parse(form["Id"]),
                StatusCode = form["StatusCode"],
                Description = form["Description"],
               OrderId = int.Parse(form["OrderId"])
            };
        }
    }
}
