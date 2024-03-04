using FarmProduct.WebApi.Models.Categories;
using FluentValidation;

namespace FarmProduct.WebApi.Validations
{
	public class CategoriesValidator : AbstractValidator<CategoriesEditModel>
	{
		public CategoriesValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty()
				.WithMessage("Tên Admin không để trống")
				.MaximumLength(100)
				.WithMessage("Tên Admin tối đa 100 ký tự");

			RuleFor(p => p.UrlSlug)
				.NotEmpty()
				.WithMessage("không để trống")
				.MaximumLength(100)
				.WithMessage("tối đa 100 ký tự");

		}
	}
}
