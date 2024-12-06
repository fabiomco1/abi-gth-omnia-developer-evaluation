using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public record GetSaleCommand : IRequest<GetSaleResult>
{
	public string SaleNumber { get; }

	public GetSaleCommand(string saleNumber)
	{
		SaleNumber = saleNumber;
	}
}
