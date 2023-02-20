using FastEndpoints;
using MinimalApi.CleanArchitecture.Application.Common.Exceptions;
using MinimalApi.CleanArchitecture.Application.Common.Interfaces;
using MinimalApi.CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;

public record UpdateTodoListCommand : ICommand
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
}

public class UpdateTodoListCommandHandler : ICommandHandler<UpdateTodoListCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly UpdateTodoListCommandMapper _mapper;

    public UpdateTodoListCommandHandler(IApplicationDbContext applicationDbContext, UpdateTodoListCommandMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(UpdateTodoListCommand command, CancellationToken ct = default)
    {
        var entity = await _applicationDbContext.TodoLists.FindAsync(new object[] { command.Id });

        if(entity == null)
        {
            throw new NotFoundException(nameof(TodoList), command.Id);
        }

        _mapper.UpdateEntity(command, entity);

        await _applicationDbContext.SaveChangesAsync(ct);
    }
}