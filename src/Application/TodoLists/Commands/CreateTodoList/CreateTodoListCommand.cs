using FastEndpoints;
using MinimalApi.CleanArchitecture.Application.Common.Interfaces;

namespace MinimalApi.CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;


public class CreateTodoListCommand : ICommand<CreateTodoListCommandResponse>
{    
    public string? Title { get; set; }
}

public class CreateTodoListCommandHandler : ICommandHandler<CreateTodoListCommand, CreateTodoListCommandResponse>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly CreateTodoListCommandMapper _mapper;

    public CreateTodoListCommandHandler(IApplicationDbContext applicationDbContext, CreateTodoListCommandMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<CreateTodoListCommandResponse> ExecuteAsync(CreateTodoListCommand command, CancellationToken cancellationToken = default)
    {
        throw new Exception();
        var todoList = await _mapper.ToEntityAsync(command);
        await _applicationDbContext.TodoLists.AddAsync(todoList);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return await _mapper.FromEntityAsync(todoList);
    }
}