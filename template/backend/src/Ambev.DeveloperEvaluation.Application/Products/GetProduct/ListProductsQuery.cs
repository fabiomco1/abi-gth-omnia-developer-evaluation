using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public record ListProductsQuery() : IRequest<List<ProductDto>>;

public record ProductDto(
	Guid Id,
	string ProductName,
	string Description,
	string Category,
	string Image,
	decimal Price,
	DateTime CreatedAt
);