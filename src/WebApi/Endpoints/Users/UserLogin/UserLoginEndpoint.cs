using MinimalApi.CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using MinimalApi.CleanArchitecture.Application.Users.UserLogin;

namespace MinimalApi.CleanArchitecture.WebApi.Endpoints.Users.UserLogin;

public class UserLoginEndpoint : Endpoint<UserLoginCommand>
{
    public override void Configure()
    {
        Post("/users/login");
        AllowAnonymous();

    }

    public override async Task HandleAsync(UserLoginCommand r, CancellationToken c)
    {
        
        var result = await r.ExecuteAsync();

        if(result)
        {
            var jwtToken = JWTBearer.CreateToken(
                signingKey: "TokenSigningKey",
                expireAt: DateTime.UtcNow.AddDays(1),
                priviledges: u =>
                {
                    u.Roles.Add("Manager");
                    u.Permissions.AddRange(new[] { "ManageUsers", "ManageInventory" });
                    u.Claims.Add(new("UserName", r.Username));
                    u["UserID"] = "001"; //indexer based claim setting
                });

            await SendAsync(new
            {
                Username = r.Username,
                Token = jwtToken
            });
        }

        await SendAsync(result);
    }
}
