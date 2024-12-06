﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleValidator : AbstractValidator<CancelSaleCommand>
{
	public CancelSaleValidator()
	{
		RuleFor(x => x.SaleNumber)
			.NotEmpty()
			.WithMessage("Sale Number is required");
	}
}
