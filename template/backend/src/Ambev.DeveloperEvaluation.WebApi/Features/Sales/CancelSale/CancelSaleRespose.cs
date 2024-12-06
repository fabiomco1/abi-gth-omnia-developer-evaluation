using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
public class CancelSaleResponse
{
	public string SaleNumber { get; set; }
	public DateTime CancelledAt { get; set; }	
}
