namespace FarmProduct.WebApi.Models.Comments
{
	public class CommentFilterModel : PagingModel
	{
		public string Name { get; set; }
        public bool? Status { get; set; }
    }
}
