using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
	public class ListSalesQuery : IRequest<List<GetSaleResult>>
	{
	}
}
