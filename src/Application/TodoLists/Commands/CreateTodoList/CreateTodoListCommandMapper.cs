using FastEndpoints;
using Mapster;
using MinimalApi.CleanArchitecture.Domain.Entities;

namespace MinimalApi.CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandMapper : Mapper<CreateTodoListCommand, CreateTodoListCommandResponse, TodoList>
{
    public override async Task<TodoList> ToEntityAsync(CreateTodoListCommand r, CancellationToken ct = default)
    {
        return r.Adapt<TodoList>();
    }

    public override async Task<CreateTodoListCommandResponse> FromEntityAsync(TodoList e, CancellationToken ct = default)
    {
        return e.Adapt<CreateTodoListCommandResponse>();
    }
}
