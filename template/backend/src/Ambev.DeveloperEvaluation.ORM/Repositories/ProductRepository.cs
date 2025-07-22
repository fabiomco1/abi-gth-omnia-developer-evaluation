using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
	private readonly DefaultContext _context;

	/// <summary>
	/// Initializes a new instance of ProductRepository
	/// </summary>
	/// <param name="context">The database context</param>
	public ProductRepository(DefaultContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Creates a new Product in the database
	/// </summary>
	/// <param name="Product">The Product to create</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created Product</returns>
	public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
	{
		await _context.Products.AddAsync(product, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		return product;
	}

	/// <summary>
	/// Retrieves a Product by their unique identifier
	/// </summary>
	/// <param name="id">The unique identifier of the Product</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The Product if found, null otherwise</returns>
	public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Products.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
	}
	public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _context.Products.ToListAsync(cancellationToken);
	}

	public async Task<List<string>> GetAllProductsAsync(CancellationToken cancellationToken = default)
	{
		return await _context.Products.Select(u => u.Id.ToString()).ToListAsync(cancellationToken);
	}
}
