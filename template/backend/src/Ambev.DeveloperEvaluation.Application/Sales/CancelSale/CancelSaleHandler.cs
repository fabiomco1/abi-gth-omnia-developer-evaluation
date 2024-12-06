using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
	private readonly ISaleRepository _saleRepository;
	private readonly IMapper _mapper;

	public CancelSaleHandler(
		ISaleRepository saleRepository,
		IMapper mapper)
	{
		_saleRepository = saleRepository;
		_mapper = mapper;
	}

	public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
	{
		var validator = new CancelSaleValidator();
		var validationResult = await validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
			throw new ValidationException(validationResult.Errors);

		var sale = await _saleRepository.GetBySaleNumberAsync(request.SaleNumber, cancellationToken);
		
		sale.Cancelled = true;
		sale.CancelledAt = DateTime.UtcNow;

		var salecanceled = await _saleRepository.CancelAsync(sale, cancellationToken);
		if (salecanceled == null)
			throw new KeyNotFoundException($"Sale with ID {request.SaleNumber} not found");

		return _mapper.Map<CancelSaleResult>(sale);
	}
}
