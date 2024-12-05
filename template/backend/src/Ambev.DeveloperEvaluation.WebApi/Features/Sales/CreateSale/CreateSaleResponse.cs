namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
	public class CreateSaleResponse
	{
		public string SaleNumber { get; set; }
		public decimal TotalSaleAmount { get; set; }
		public List<SaleProductResponse> SalesProducts { get; set; }
	}

	public class SaleProductResponse
	{
		public string ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal TotalItemAmount { get; set; }
		public decimal Discount { get; set; }
	}
}