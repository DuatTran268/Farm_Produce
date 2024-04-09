﻿using FarmProduct.WebApi.Models.Unit;
using FluentValidation;

namespace FarmProduct.WebApi.Validations
{
	public class UnitValidator : AbstractValidator<UnitEditModel>
	{
		public UnitValidator()
		{
			RuleFor(p => p.Name)
					.NotEmpty()
					.WithMessage("Tên tiến trình thực hiện không để trống")
					.MaximumLength(100)
					.WithMessage("Tên tiến trình thực hiện tối đa 100 ký tự");

			RuleFor(p => p.UrlSlug)
				.NotEmpty()
				.WithMessage("UrlSlug không để trống")
				.MaximumLength(100)
				.WithMessage("UrlSlug tối đa 100 ký tự");
		}
	}
}