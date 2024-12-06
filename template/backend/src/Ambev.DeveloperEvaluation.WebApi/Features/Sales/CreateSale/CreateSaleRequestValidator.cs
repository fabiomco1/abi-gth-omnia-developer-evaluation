using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
	public CreateSaleRequestValidator()
	{
		RuleFor(sale => sale.Customer).NotEmpty().WithMessage("O cliente é obrigatório.");
		RuleFor(sale => sale.SaleDate).NotEmpty().WithMessage("A data da venda é obrigatória.");
		RuleFor(sale => sale.SaleNumber).NotEmpty().WithMessage("O número da nota é obrigatório");
	}
}
