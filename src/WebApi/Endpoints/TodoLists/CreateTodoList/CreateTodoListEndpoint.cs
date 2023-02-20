using MinimalApi.CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;

namespace WebApi.TodoLists.CreateTodoList;

public class Endpoints : Endpoint<CreateTodoListCommand, CreateTodoListCommandResponse>
{
    public override void Configure()
    {
        Post("/todoitems");
        AllowAnonymous();

    }

    public override async Task HandleAsync(CreateTodoListCommand r, CancellationToken c)
    {
        var result = await r.ExecuteAsync();

        await SendAsync(result);
    }
}