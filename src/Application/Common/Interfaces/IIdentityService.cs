using Microsoft.AspNetCore.Identity;
using MinimalApi.CleanArchitecture.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.CleanArchitecture.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<SignInResult> SignInAsync(string userName, string password, bool isPersistent = false, bool lockoutOnFailure = false);

    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
}
