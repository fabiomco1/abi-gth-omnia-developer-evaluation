﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public ProductsController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpPost]
	[ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
	{
		try
		{
			var command = _mapper.Map<CreateProductCommand>(request);
			var response = await _mediator.Send(command, cancellationToken);

			return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
			{
				Success = true,
				Message = "Product created successfully",
				Data = _mapper.Map<CreateProductResponse>(response)
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { Success = false, Message = "An error occurred while creating the Product", Error = ex.Message });
		}
	}
}
