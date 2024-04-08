using FarmProduce.Core.Entities;

namespace FarmProduct.WebApi.Models.Comments
{
	public class CommentDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Rating { get; set; }
		public DateTime Created { get; set; } = DateTime.Now;
		public string CommentText { get; set; }
		public bool Status { get; set; }
		public string UserId { get; set; }
		//public Customer Customer { get; set; }
	}
}
