using FarmProduct.WebApi.Models.Admin;
using FluentValidation;


namespace FarmProduct.WebApi.Validations
{
	public class AdminValidator : AbstractValidator<AdminEditModel>
	{
		public AdminValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty()
				.WithMessage("Tên Admin không để trống")
				.MaximumLength(100)
				.WithMessage("Tên Admin tối đa 100 ký tự");

			RuleFor(p => p.Email)
				.NotEmpty()
				.WithMessage("không để trống")
				.MaximumLength(100)
				.WithMessage("tối đa 100 ký tự");

			RuleFor(p => p.Password)
				.NotEmpty()
				.WithMessage("không để trống")
				.MaximumLength(100)
				.WithMessage("tối đa 100 ký tự");

			RuleFor(p => p.Role)
				.NotEmpty()
				.WithMessage("không để trống")
				.MaximumLength(100)
				.WithMessage("tối đa 100 ký tự");

		}
	}
}
