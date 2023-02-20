using FastEndpoints;
using MinimalApi.CleanArchitecture.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.CleanArchitecture.Application.Users.UserLogin;

public record UserLoginCommand : ICommand<bool>
{
    public string Username { get; init; }
    public string Password { get; init; }
}


public class UserLoginCommandHandler : ICommandHandler<UserLoginCommand, bool>
{
    private readonly IIdentityService _identityService;
    public UserLoginCommandHandler(IIdentityService identityService)
    {
        _identityService= identityService;
    }

    public async Task<bool> ExecuteAsync(UserLoginCommand command, CancellationToken ct = default)
    {
        var result = await _identityService.SignInAsync(command.Username, command.Password);
        return result.Succeeded;
    }
}