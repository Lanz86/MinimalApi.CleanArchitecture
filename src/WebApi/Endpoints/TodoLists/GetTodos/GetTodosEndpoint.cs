using MinimalApi.CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using MinimalApi.CleanArchitecture.Application.TodoLists.Queries.GetTodos;

namespace WebApi.Endpoints.TodoLists.GetTodos
{
    public class GetTodosEndpoint : Endpoint<GetTodosQuery, IEnumerable<GetTodosItemQueryResponse>>
    {
        public override void Configure()
        {
            Get("/todoitems");
            AllowAnonymous();

        }

        public override async Task HandleAsync(GetTodosQuery r, CancellationToken c)
        {
            var result = await r.ExecuteAsync();

            await SendAsync(result);
        }
    }
}
