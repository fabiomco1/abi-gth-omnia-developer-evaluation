﻿using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesProducts.CreateSaleProduct;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
public class CreateSaleRequest
{
	public string SaleNumber { get; set; } = string.Empty;
	public DateTime? SaleDate { get; set; }
	public string Customer { get; set; } = string.Empty;
	public string Branch { get; set; } = string.Empty;
	public List<CreateSaleProductRequest> SalesProducts { get; set; } = new List<CreateSaleProductRequest>();
}