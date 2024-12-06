using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleValidator : AbstractValidator<GetSaleCommand>
{
	public GetSaleValidator()
	{
		RuleFor(x => x.SaleNumber)
			.NotEmpty()
			.WithMessage("Sale Number is required");
	}
}
