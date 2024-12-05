using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class CreateProductCommand : IRequest<CreateProductResult>
{
	public string ProductName { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public DateTime CreatedAt { get; set; }
}


