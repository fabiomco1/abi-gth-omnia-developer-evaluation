using Ambev.DeveloperEvaluation.Application.SalesProducts.CreateSaleProducts;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
	public string SaleNumber { get; set; } = string.Empty;
	public DateTime? SaleDate { get; set; }
	public string Customer { get; set; } = string.Empty;
	public decimal TotalSaleAmount { get; set; }
	public string Branch { get; set; } = string.Empty;
	public bool Cancelled { get; set; }
	public DateTime CreatedAt { get; set; }
	public List<CreateSaleProductCommand> SalesProducts { get; set; } = new List<CreateSaleProductCommand>();

}

