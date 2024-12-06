using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using FluentAssertions;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
namespace YourProject.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateProductHandler"/> class.
/// </summary>
public class CreateProductHandlerTests
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;
	private readonly CreateProductHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="CreateProductHandlerTests"/> class.
	/// Sets up the test dependencies.
	/// </summary>
	public CreateProductHandlerTests()
	{
		_productRepository = Substitute.For<IProductRepository>();
		_mapper = Substitute.For<IMapper>();
		_handler = new CreateProductHandler(_productRepository, _mapper);
	}

	/// <summary>
	/// Tests that a valid product creation request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Teste de Produto")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = new CreateProductCommand
		{
			ProductName = "PRODUTO TESTE UNITARIO",
			Description = "TESTE DE INCLUSAO DE PRODUTO",
			Category = "SUCO",
			Image = "TESTE.jpg",
			Price = 10.50M
		};

		var product = new Product
		{
			//Id = Guid.NewGuid(),
			ProductName = command.ProductName,
			Description = command.Description,
			Category = command.Category,
			Image = command.Image,
			Price = command.Price,
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow
		};

		_mapper.Map<Product>(command).Returns(product);
		_productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
			.Returns(Task.FromResult(product));

		// When
		var result = await _handler.Handle(command, CancellationToken.None);

		// Then
		result.Should().NotBeNull();
	//	result.Id.Should().Be(product.Id);
		await _productRepository.Received(1).CreateAsync(Arg.Is<Product>(p =>
			p.ProductName == command.ProductName &&
			p.Description == command.Description &&
			p.Category == command.Category &&
			p.Price == command.Price),
			Arg.Any<CancellationToken>());
	}


	[Fact(DisplayName = "Produto Invalido")]
	public async Task Handle_InvalidRequest_ThrowsValidationException()
	{
		// Given
		var command = new CreateProductCommand(); 

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}


	[Fact(DisplayName = "Testa o mapeamento da entidade do produto")]
	public async Task Handle_ValidRequest_MapsCommandToProduct()
	{
		var command = new CreateProductCommand
		{
			ProductName = "Produto Mapeado",
			Description = "Test Produto Mapeado",
			Category = "Refresco",
			Image = "suco.jpg",
			Price = 1.99M
		};

		var product = new Product
		{
			Id = Guid.NewGuid(),
			ProductName = command.ProductName,
			Description = command.Description,
			Category = command.Category,
			Image = command.Image,
			Price = command.Price
		};

		_mapper.Map<Product>(command).Returns(product);
		_productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
			.Returns(Task.FromResult(product));

		// When
		await _handler.Handle(command, CancellationToken.None);

		// Then
		_mapper.Received(1).Map<Product>(Arg.Is<CreateProductCommand>(c =>
			c.ProductName == command.ProductName &&
			c.Description == command.Description &&
			c.Category == command.Category &&
			c.Image == command.Image &&
			c.Price == command.Price));
	}
}