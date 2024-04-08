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
<<<<<<< HEAD
		public int CustomerId { get; set; }
		public int ProductId { get; set; }
=======
		public string UserId { get; set; }
>>>>>>> 88fc07b067fd5a878f0771f452ecb77c6ad75ae7
		//public Customer Customer { get; set; }
	}
}
