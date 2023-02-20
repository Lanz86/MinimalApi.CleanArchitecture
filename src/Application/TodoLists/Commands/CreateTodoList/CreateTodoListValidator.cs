using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MinimalApi.CleanArchitecture.Application.Common.Interfaces;

namespace MinimalApi.CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;


public class CreateTodoListValidator : Validator<CreateTodoListCommand>
{
    public CreateTodoListValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await Resolve<IApplicationDbContext>().TodoLists
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}