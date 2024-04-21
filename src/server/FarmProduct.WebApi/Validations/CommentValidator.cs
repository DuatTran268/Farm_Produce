using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Unit;
using FluentValidation;

namespace FarmProduct.WebApi.Validations
{
	public class CommentValidator : AbstractValidator<CommentEditModel>
	{
		public CommentValidator()
		{

			RuleFor(p => p.Name)
					.NotEmpty()
					.WithMessage("Không để trống")
					.MaximumLength(100)
					.WithMessage("Tối đa 100 ký tự");


			RuleFor(p => p.Rating)
					.NotEmpty();	
			RuleFor(u => u.Created)
					.GreaterThan(DateTime.MinValue)
					.WithMessage("Ngày không hợp lệ");
			
			RuleFor(p => p.CommentText)
					.NotEmpty()
					.WithMessage("Không được để trống")
					.MaximumLength(1000)
					.WithMessage("Tối đa 3000 ký tự");
		}
	}
}
