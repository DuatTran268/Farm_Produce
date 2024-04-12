using System.Globalization;

namespace FarmProduct.WebApi.Models.Products
{
    public class ProductEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuanlityAvailable { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; } = 0;
        public string Description { get; set; }
        public bool Status { get; set; }
        public int UnitId { get; set; }
        //public DateTime DateCreate { get; set; } = DateTime.Now; 
        //public DateTime DateUpdate { get; set; } = DateTime.Now;
        public static async ValueTask<ProductEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            return new ProductEditModel()
            {
                Id = int.Parse(form["Id"]),
                Name = form["Name"],
                Description = form["Description"],
                QuanlityAvailable = int.Parse(form["QuanlityAvailable"]),
                CategoryId = int.Parse(form["CategoryId"]),
                Price = decimal.Parse(form["Price"]),
                Status = form["Status"] !="false",
                UnitId = int.Parse(form["UnitId"]),
				//DateCreate = DateTime.Parse(form["DateCreate"]),
				//DateUpdate = DateTime.Parse(form["DateUpdate"]),
			};
        }
    }
}
