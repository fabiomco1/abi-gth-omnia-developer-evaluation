using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesProducts.CreateSaleProduct;


	public class CreateSaleProductRequest
	{
	//public string Id { get; set; } = string.Empty;
//	public string SaleNumber { get; set; } = string.Empty;
	public string ProductId { get; set; } = string.Empty;
	public DateTime SaleDate { get; set; }
	public int Quantity { get; set; }
	public decimal Discount { get; set; }

}

