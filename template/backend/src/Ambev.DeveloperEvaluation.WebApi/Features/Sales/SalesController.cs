using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesProducts.CreateSaleProduct;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Bogus;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	private readonly IUserRepository _userRepository;
	private readonly IProductRepository _productRepository;

	public SalesController(IMediator mediator, IMapper mapper, IUserRepository userRepository, IProductRepository productRepository)
	{
		_userRepository = userRepository;
		_productRepository = productRepository;
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpPost("SaleCreated")]
	[ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
	{
		var validator = new CreateSaleRequestValidator();
		var validationResult = await validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
			return BadRequest(validationResult.Errors);

		try
		{
			var command = _mapper.Map<CreateSaleCommand>(request);
			var response = await _mediator.Send(command, cancellationToken);

			return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
			{
				Success = true,
				Message = "Sale created successfully",
				Data = _mapper.Map<CreateSaleResponse>(response)
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { Success = false, Message = "An error occurred while creating the sale", Error = ex.Message });
		}
	}

	[HttpGet("GetSale/{id}")]
	[ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetSale([FromRoute] string id, CancellationToken cancellationToken)
	{
		var request = new GetSaleRequest { SaleNumber = id };
		var validator = new GetSaleRequestValidator();
		var validationResult = await validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
			return BadRequest(validationResult.Errors);

		var command = _mapper.Map<GetSaleCommand>(request.SaleNumber);
		var response = await _mediator.Send(command, cancellationToken);

		return Ok(new ApiResponseWithData<GetSaleResponse>
		{
			Success = true,
			Message = "Sale retrieved successfully",
			Data = _mapper.Map<GetSaleResponse>(response)
		});
	}

	[HttpGet("SaleCancelled/{id}")]
	[ProducesResponseType(typeof(ApiResponseWithData<CancelSaleResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> SaleCancelled([FromRoute] string id, CancellationToken cancellationToken)
	{
		var request = new CancelSaleRequest { SaleNumber = id };
		var validator = new CancelSaleRequestValidator();
		var validationResult = await validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
			return BadRequest(validationResult.Errors);

		var command = _mapper.Map<CancelSaleCommand>(request.SaleNumber);
		var response = await _mediator.Send(command, cancellationToken);

		return Ok(new ApiResponseWithData<CancelSaleResponse>
		{
			Success = true,
			Message = "Sale Cancelled successfully",
			Data = _mapper.Map<CancelSaleResponse>(response)
		});
	}

	[HttpGet("ListAll")]
	public async Task<IActionResult> ListAllSales()
	{
		try
		{
			var result = await _mediator.Send(new ListSalesQuery());
			return Ok(new { success = true, data = result });
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				success = false,
				message = "An error occurred while listing sales",
				error = ex.Message
			});
		}
	}

	[HttpPost("CreateSaleTest")]
	[ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateFakeSale(CancellationToken cancellationToken)
	{
		try
		{
			var userNames = await _userRepository.GetAllUserAsync(cancellationToken);

			var productsId = await _productRepository.GetAllProductsAsync(cancellationToken);

			var bogusfakeproduct = new Faker<CreateSaleProductRequest>()
				.RuleFor(p => p.ProductId, f => productsId[f.Random.Int(0, productsId.Count - 1)])
				.RuleFor(p => p.Quantity, f => f.Random.Int(1, 10));

			var bogusfakesale = new Faker<CreateSaleRequest>()
				.RuleFor(u => u.SaleNumber, f => f.Commerce.Ean8())
				.RuleFor(u => u.SaleDate, f => DateTime.UtcNow)
				.RuleFor(u => u.Customer, f => userNames[f.Random.Int(0, userNames.Count - 1)])
				.RuleFor(u => u.Branch, f => f.Company.CompanyName())
				.RuleFor(u => u.SalesProducts, f => bogusfakeproduct.Generate(f.Random.Int(1, 5)));

			var salerequest = bogusfakesale.Generate(3);

			foreach (var request in salerequest)
			{
				var command = _mapper.Map<CreateSaleCommand>(request);
				var response = await _mediator.Send(command, cancellationToken);
			}


			return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
			{
				Success = true,
				Message = "Sales created successfully"
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { Success = false, Message = "An error occurred while creating the Product", Error = ex.Message });
		}
	}
}