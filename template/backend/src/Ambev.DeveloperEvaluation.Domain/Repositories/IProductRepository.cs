using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
	Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);
	Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
	Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
	Task<List<string>> GetAllProductsAsync(CancellationToken cancellationToken = default);
}