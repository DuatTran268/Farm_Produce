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
        public DateTime DateCreate { get; set; } = DateTime.Now; 
        public DateTime DateUpdate { get; set; } = DateTime.Now;
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
				DateCreate = ParseDateTime(form["DateCreate"]),
				DateUpdate = ParseDateTime(form["DateUpdate"]),
			};
        }
        private static DateTime ParseDateTime(string dateTimeString)
        {
            if (string.IsNullOrWhiteSpace(dateTimeString))
            {
                // Return a default value or handle the empty case according to your logic
                return DateTime.MinValue;
            }

            string[] formats = { "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd" }; // Add more formats as needed

            if (DateTime.TryParseExact(dateTimeString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
            {
                return parsedDateTime;
            }
            else
            {
                throw new FormatException("Invalid DateTime format");
            }
        }
    }
}
