using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleProductRepository
{
	Task<SaleProduct> CreateAsync(SaleProduct saleproduct, CancellationToken cancellationToken = default);
	Task<SaleProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

}
