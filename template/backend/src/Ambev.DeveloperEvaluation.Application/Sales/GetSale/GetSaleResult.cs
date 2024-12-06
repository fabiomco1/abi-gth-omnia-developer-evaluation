namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;
public class GetSaleResult
{
	public Guid Id { get; set; }
	public string SaleNumber { get; set; } = string.Empty;
	public DateTime? SaleDate { get; set; }
	public string Customer { get; set; } = string.Empty;
	public Decimal TotalSaleAmount { get; set; }
	public int TotalItems { get; set; }
	public string Branch { get; set; } = string.Empty;
}
