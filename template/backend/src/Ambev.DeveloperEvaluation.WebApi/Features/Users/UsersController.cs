using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using AutoMapper;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users;

/// <summary>
/// Controller for managing user operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of UsersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="request">The user creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateUserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        try
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateUserResponse>
            {
                Success = true,
                Message = "User created successfully",
                Data = _mapper.Map<CreateUserResponse>(response)
            });
        }
		catch (Exception ex)
		{
			return StatusCode(500, new { Success = false, Message = "An error occurred while creating the user", Error = ex.Message });
		}
	}
	[HttpGet("ListAll")]
	public async Task<IActionResult> ListAllUsers()
	{
		try
		{
			var result = await _mediator.Send(new ListUsersQuery());
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
	/// <summary>
	/// Retrieves a user by their ID
	/// </summary>
	/// <param name="id">The unique identifier of the user</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The user details if found</returns>
	[HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetUserRequest { Id = id };
        var validator = new GetUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetUserCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetUserResponse>
        {
            Success = true,
            Message = "User retrieved successfully",
            Data = _mapper.Map<GetUserResponse>(response)
        });
    }

    /// <summary>
    /// Deletes a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the user was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteUserRequest { Id = id };
        var validator = new DeleteUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteUserCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "User deleted successfully"
        });
    }
	[HttpPost("CreatetUserTest")]
	[ProducesResponseType(typeof(ApiResponseWithData<CreateUserResponse>), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateUserTest(CancellationToken cancellationToken)
	{
		try
		{
			var validStatuses = Enum.GetValues(typeof(UserStatus)).Cast<UserStatus>()
	            .Where(status => status != UserStatus.Unknown).ToArray();

			var validRoles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>()
				.Where(role => role != UserRole.None).ToArray();

			var bogusfake = new Faker<CreateUserRequest>()
                .RuleFor(u => u.Username, f => f.Name.FirstName() + "_" + f.Name.LastName())
                .RuleFor(u => u.Email,    f => f.Internet.Email())
            	.RuleFor(u => u.Status,   f => f.PickRandom(validStatuses))
	            .RuleFor(u => u.Role,     f => f.PickRandom(validRoles))
				.RuleFor(u => u.Phone,    f =>
                {
                    var ddd = f.Random.Number(11, 99);
                    var prefix = f.Random.Number(90000, 99999);
                    var suffix = f.Random.Number(1000, 9999);
                    return $"+55{ddd}{prefix}{suffix}";
                })
                .RuleFor(u => u.Password, f =>
                {
	                var maiuscula = f.Random.AlphaNumeric(1).ToUpper();
	                var numeros = f.Random.Number(0, 9).ToString();
	                var caracterespecial = "#$^*";
	                var especial = caracterespecial[f.Random.Int(0, caracterespecial.Length - 1)];
					var minuscula = f.Random.AlphaNumeric(8);
					return numeros + especial + maiuscula + minuscula;
                });

			var userRequests = bogusfake.Generate(3);

            foreach (var request in userRequests)
            {
                var command = _mapper.Map<CreateUserCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);
            }

			return Created(string.Empty, new ApiResponseWithData<CreateUserResponse>
			{
				Success = true,
				Message = "Users created successfully"
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { Success = false, Message = "An error occurred while creating the user", Error = ex.Message });
		}
	}
}
