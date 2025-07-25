﻿using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
	Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
	Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
	Task<Sale?> GetBySaleNumberAsync(string? SaleNumber, CancellationToken cancellationToken = default);
	Task<Sale?> CancelAsync(Sale? sale, CancellationToken cancellationToken = default);
	Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}