﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
	private readonly DefaultContext _context;

	public SaleRepository(DefaultContext context)
	{
		_context = context;
	}

	public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
	{
		await _context.Sales.AddAsync(sale, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		return sale;
	}
	public async Task<Sale> CancelAsync(Sale sale, CancellationToken cancellationToken = default)
	{
		_context.Entry(sale).Property(s => s.Cancelled).IsModified = true;
		_context.Entry(sale).Property(s => s.CancelledAt).IsModified = true;

		await _context.SaveChangesAsync(cancellationToken);
		return sale;
	}

	public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Sales.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
	}
	public async Task<Sale?> GetBySaleNumberAsync(string? saleNumber, CancellationToken cancellationToken = default)
	{
		return await _context.Sales.FirstOrDefaultAsync(o => o.SaleNumber == saleNumber, cancellationToken);
	}
	public async Task<Sale?> GetBySaleAsync(string sale, CancellationToken cancellationToken = default)
	{
		return await _context.Sales
			.FirstOrDefaultAsync(u => u.Customer == sale, cancellationToken);
	}

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
