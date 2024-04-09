namespace FarmProduct.WebApi.Models.Categories
{
	public class CategoriesEditModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
		//public string UrlSlug { get; set; }
		// image

		public IFormFile ImageFile { get; set; }

		public static async ValueTask<CategoriesEditModel> BindAsync(HttpContext context)
		{
			var form = await context.Request.ReadFormAsync();
			return new CategoriesEditModel()
			{
				Id = int.Parse(form["Id"]),
				Name = (form["Name"]),
				//UrlSlug = (form["UrlSlug"]),
				ImageFile = form.Files["ImageFile"]
			};
		}
	}
}
