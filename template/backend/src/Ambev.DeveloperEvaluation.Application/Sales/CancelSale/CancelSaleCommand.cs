using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public record CancelSaleCommand : IRequest<CancelSaleResult>
{
	public string SaleNumber { get; }

	public CancelSaleCommand(string saleNumber)
	{
		SaleNumber = saleNumber;
	}
}
