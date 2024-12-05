using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
	private readonly ISaleRepository _saleRepository;
	private readonly IMapper _mapper;
	private readonly IProductRepository _productRepository;

	public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IProductRepository productRepository)
	{
		_saleRepository = saleRepository;
		_mapper = mapper;
		_productRepository = productRepository;
	}


	/// <summary>
	/// Handles the CreateUserCommand request
	/// </summary>
	/// <param name="command">The CreateUser command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created user details</returns>
	public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
	{
		//	var validator = new CreateSaleCommandValidator();
		//	var validationResult = await validator.ValidateAsync(command, cancellationToken);

		//if (!validationResult.IsValid)
		//		throw new ValidationException(validationResult.Errors);

		//		var existingSale = await _saleRepository.GetByEmailAsync(command.Email, cancellationToken);
		//		if (existingSale != null)
		//			throw new InvalidOperationException($"User with email {command.Email} already exists");

		var sale = _mapper.Map<Sale>(command);

		foreach (var sp in sale.SalesProducts)
		{
			// Busca o produto no repositório
			var product = await _productRepository.GetByIdAsync(sp.ProductId, cancellationToken);

			if (product == null)
			{
				throw new InvalidOperationException($"Product with ID {sp.ProductId} not found");
			}

			// Calcula o TotalItemAmount para o produto
			sp.TotalItemAmount = sp.Quantity * product.Price;
		}

		var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
	var result = _mapper.Map<CreateSaleResult>(createdSale);
	return result;
	}
}
