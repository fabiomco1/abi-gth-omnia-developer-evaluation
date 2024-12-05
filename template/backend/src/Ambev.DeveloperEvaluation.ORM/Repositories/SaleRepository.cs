using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
	private readonly DefaultContext _context;

	/// <summary>
	/// Initializes a new instance of UserRepository
	/// </summary>
	/// <param name="context">The database context</param>
	public SaleRepository(DefaultContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Creates a new user in the database
	/// </summary>
	/// <param name="user">The user to create</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created user</returns>
	public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
	{
		await _context.Sales.AddAsync(sale, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		return sale;
	}

	/// <summary>
	/// Retrieves a user by their unique identifier
	/// </summary>
	/// <param name="id">The unique identifier of the user</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The user if found, null otherwise</returns>
	public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Sales.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
	}

	/// <summary>
	/// Retrieves a user by their email address
	/// </summary>
	/// <param name="email">The email address to search for</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The user if found, null otherwise</returns>
	public async Task<Sale?> GetBySaleAsync(string sale, CancellationToken cancellationToken = default)
	{
		return await _context.Sales
			.FirstOrDefaultAsync(u => u.Customer == sale, cancellationToken);
	}

	/// <summary>
	/// Deletes a user from the database
	/// </summary>
	/// <param name="id">The unique identifier of the user to delete</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if the user was deleted, false if not found</returns>
	public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var sale = await GetByIdAsync(id, cancellationToken);
		if (sale == null)
			return false;

		_context.Sales.Remove(sale);
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}
}
