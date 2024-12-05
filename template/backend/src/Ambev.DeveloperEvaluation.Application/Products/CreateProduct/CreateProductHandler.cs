using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

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
		//	var validator = new CreateSaleCommandValidator();
		//	var validationResult = await validator.ValidateAsync(command, cancellationToken);

		//if (!validationResult.IsValid)
		//		throw new ValidationException(validationResult.Errors);

		//		var existingSale = await _saleRepository.GetByEmailAsync(command.Email, cancellationToken);
		//		if (existingSale != null)
		//			throw new InvalidOperationException($"User with email {command.Email} already exists");


		var product = _mapper.Map<Product>(command);

		var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
		var result = _mapper.Map<CreateProductResult>(createdProduct);

		return result;
	}
}
