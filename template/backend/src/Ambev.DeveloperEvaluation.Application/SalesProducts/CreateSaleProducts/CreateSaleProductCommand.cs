using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesProducts.CreateSaleProducts;

public class CreateSaleProductCommand : IRequest<CreateSaleProductResult>
{
	public Guid Id { get; set; }
	public Guid SaleNumber { get; set; }
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
	public decimal Discount { get; set; }
}


