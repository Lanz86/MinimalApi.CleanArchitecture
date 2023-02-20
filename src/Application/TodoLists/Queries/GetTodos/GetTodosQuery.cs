using FastEndpoints;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MinimalApi.CleanArchitecture.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.CleanArchitecture.Application.TodoLists.Queries.GetTodos;

public class GetTodosQuery : ICommand<IEnumerable<GetTodosItemQueryResponse>>
{
}

public class GetTodosQueryHandler : ICommandHandler<GetTodosQuery, IEnumerable<GetTodosItemQueryResponse>>
{
	private readonly IApplicationDbContext _applicationDbContext;
	public GetTodosQueryHandler(IApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}

	public async Task<IEnumerable<GetTodosItemQueryResponse>> ExecuteAsync(GetTodosQuery command, CancellationToken ct = default)
    {
		return await _applicationDbContext.TodoLists.AsNoTracking().ProjectToType<GetTodosItemQueryResponse>().ToListAsync();
    }
}
