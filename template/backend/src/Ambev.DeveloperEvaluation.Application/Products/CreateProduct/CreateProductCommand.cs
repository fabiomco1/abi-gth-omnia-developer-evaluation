using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using MediatR;

public class CreateProductCommand : IRequest<CreateProductResult>
{
	public string ProductName { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Category { get; set; } = string.Empty;
	public string Image { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public DateTime CreatedAt { get; set; }
}


