using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleResponse
{
	public string SaleNumber { get; set; }
	public string Branch { get; set; }
	public string Customer { get; set; }
	public DateTime SaleDate { get; set; }	
	public decimal TotalSaleAmount { get; set; }
	public int TotalItems { get; set; }
}
