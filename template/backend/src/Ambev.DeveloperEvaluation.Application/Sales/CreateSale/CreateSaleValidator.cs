using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
	public CreateSaleCommandValidator()
	{
		RuleFor(sale => sale.Customer).NotEmpty().WithMessage("O cliente é obrigatório.");
		RuleFor(sale => sale.SaleDate).NotEmpty().WithMessage("A data da venda é obrigatória.");
		RuleFor(sale => sale.TotalSaleAmount).GreaterThan(0).WithMessage("O valor total da venda deve ser maior que zero.");
	}
}