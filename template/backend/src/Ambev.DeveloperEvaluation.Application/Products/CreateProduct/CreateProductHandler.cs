﻿using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;

	public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
	{
		_productRepository = productRepository;
		_mapper = mapper;
	}

	public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{ 
		var product = _mapper.Map<Product>(command);
		
		var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
		var result = _mapper.Map<CreateProductResult>(createdProduct);

		return result;
	}
}
