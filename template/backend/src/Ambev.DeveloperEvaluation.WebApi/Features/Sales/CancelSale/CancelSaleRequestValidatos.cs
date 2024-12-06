﻿using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
{
	public CancelSaleRequestValidator()
	{
		RuleFor(x => x.SaleNumber)
			.NotEmpty()
			.WithMessage("Sale ID is required");
	}
}
