namespace FarmProduct.WebApi.Models.Comments
{
	public class CommentEditModel 
	{
		public string Name { get; set; }
		public int Rating { get; set; }
		public DateTime Created { get; set; } = DateTime.Now;
		public string CommentText { get; set; }
		public bool? Status { get; set; } = false;
		public int? CustomerId { get; set; }
		public int? ProductId { get; set; }



	}
}
