using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesProducts.CreateSaleProducts;

public class CreateSaleProductCommand : IRequest<CreateSaleProductResult>
{
	public Guid Id { get; set; }
//	public string SaleNumber { get; set; } = string.Empty;
	public Guid ProductId { get; set; }
	public DateTime SaleDate { get; set; }
	public int Quantity { get; set; }
	public decimal Discount { get; set; }
}


