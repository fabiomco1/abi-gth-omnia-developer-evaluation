using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

	public class SaleProduct : BaseEntity
	{
	public Guid Id { get; set; }
	public Guid SaleNumber { get; set; }
	public Guid ProductId { get; set; }
	public decimal TotalItemAmount { get; set; }
	public int Quantity { get; set; }
	public decimal Discount { get; set; }
	public Sale Sale { get; set; }
}