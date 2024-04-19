using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Discounts;
using FluentValidation;

namespace FarmProduct.WebApi.Validations
{
	public class DiscountValidator : AbstractValidator<DiscountEditModel>
	{
		public DiscountValidator() 
		{
			RuleFor(p => p.DiscountPrice)
					.NotEmpty();

			RuleFor(u => u.StartDate)
					.GreaterThan(DateTime.MinValue)
					.WithMessage("Ngày không hợp lệ");

			RuleFor(u => u.EndDate)
					.GreaterThan(DateTime.MinValue)
					.WithMessage("Ngày không hợp lệ");

			RuleFor(p => p.CodeName)
					.NotEmpty()
					.WithMessage("CodeName không để trống")
					.MaximumLength(100)
					.WithMessage("CodeName tối đa 100 ký tự");

		}
	}
}
