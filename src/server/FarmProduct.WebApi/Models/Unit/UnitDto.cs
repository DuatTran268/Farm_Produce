using FarmProduce.Core.Entities;

namespace FarmProduct.WebApi.Models.Unit
{
    public class UnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public IList<Product> Products { get; set; }
    }
}
