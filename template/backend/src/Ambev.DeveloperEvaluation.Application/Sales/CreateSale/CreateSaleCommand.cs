using Ambev.DeveloperEvaluation.Application.SalesProducts.CreateSaleProducts;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
	public string SaleNumber { get; set; } = string.Empty;
	public DateTime? SaleDate { get; set; }
	public string Customer { get; set; } = string.Empty;
	public decimal TotalSaleAmount { get; set; }
	public int TotalItems { get; set; }
	public string Branch { get; set; } = string.Empty;
	public bool Cancelled { get; set; }
	public DateTime CreatedAt { get; set; }
	public List<CreateSaleProductCommand> SalesProducts { get; set; } = new List<CreateSaleProductCommand>();

	public ValidationResultDetail Validate()
	{
		var validator = new CreateSaleCommandValidator();
		var result = validator.Validate(this);
		return new ValidationResultDetail
		{
			IsValid = result.IsValid,
			Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
		};
	}
}