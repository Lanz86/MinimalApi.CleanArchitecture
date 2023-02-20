using MinimalApi.CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;
using MinimalApi.CleanArchitecture.Application.TodoLists.Queries.GetTodos;

namespace MinimalApi.CleanArchitecture.WebApi.Endpoints.TodoLists.UpdateTodoList;

public class UpdateTodoListEndpoint : Endpoint<UpdateTodoListCommand>
{
    public override void Configure()
    {
        Put("/todoitems/{id}");
        AllowAnonymous();

    }

    public override async Task HandleAsync(UpdateTodoListCommand r, CancellationToken c)
    {
        await r.ExecuteAsync(c);
    }
}