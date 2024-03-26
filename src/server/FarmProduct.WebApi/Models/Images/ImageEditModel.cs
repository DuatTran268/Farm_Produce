namespace FarmProduct.WebApi.Models.Images
{
    public class ImageEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IFormFile ImageFile { get; set; }
        public string Caption { get; set; }
        public int ProductId { get; set; }

        public static async ValueTask<ImageEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            return new ImageEditModel()
            {
                Id = int.Parse(form["Id"]),
                Name = form["Name"],
                Caption= form["Caption"],
                ImageFile = form.Files["ImageFile"],
                ProductId = int.Parse(form["ProductId"])
            };
        }
    }
}
