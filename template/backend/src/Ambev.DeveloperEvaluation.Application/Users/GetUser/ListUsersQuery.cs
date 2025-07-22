using MediatR;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
	public class ListUsersQuery : IRequest<List<GetUserResult>>
	{
	}
}
