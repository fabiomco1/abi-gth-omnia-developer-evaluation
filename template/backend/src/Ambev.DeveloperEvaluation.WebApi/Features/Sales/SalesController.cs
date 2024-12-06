using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public SalesController(IMediator mediator, IMapper mapper)
	{
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
}