using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, List<GetUserResult>>
{
	private readonly IUserRepository _repository;
	private readonly IMapper _mapper;

	public ListUsersQueryHandler(IUserRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<List<GetUserResult>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
	{
		var sales = await _repository.GetAllAsync(cancellationToken);
		return _mapper.Map<List<GetUserResult>>(sales);
	}
}
