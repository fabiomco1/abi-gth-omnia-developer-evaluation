using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesProducts.CreateSaleProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;


public class CreateSaleRequest
{
	public string SaleNumber { get; set; } = string.Empty;
	public DateTime? SaleDate { get; set; }
	public string Customer { get; set; } = string.Empty;
	public decimal TotalSaleAmount { get; set; }
	public string Branch { get; set; } = string.Empty;
	public bool Cancelled { get; set; }
	public DateTime CreatedAt { get; set; }
	public List<CreateSaleProductRequest> SalesProducts { get; set; } = new List<CreateSaleProductRequest>();
}