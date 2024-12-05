using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Common;
using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

	public class SaleProduct : BaseEntity
	{
	public Guid Id { get; set; } 
	public Guid ProductId { get; set; }
	public decimal TotalItemAmount { get; set; }
	public int Quantity { get; set; }
	public decimal Discount { get; set; }

	// Relacionamento
	public Sale Sale { get; set; }
}

  
