using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
	/// <summary>
	/// Creates a new user in the repository
	/// </summary>
	/// <param name="sale">The user to create</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created user</returns>
	Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a user by their unique identifier
	/// </summary>
	/// <param name="id">The unique identifier of the user</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The user if found, null otherwise</returns>
	Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a user by their email address
	/// </summary>
	/// <param name="SaleNumber">The email address to search for</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The user if found, null otherwise</returns>
	//Task<Sale?> GetBySaleAsync(string SaleNumber, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes a user from the repository
	/// </summary>
	/// <param name="id">The unique identifier of the user to delete</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if the user was deleted, false if not found</returns>
	Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
