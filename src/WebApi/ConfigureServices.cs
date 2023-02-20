using MinimalApi.CleanArchitecture.Application.Common.Interfaces;
using WebApi.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddFastEndpoints(o => {
            o.SourceGeneratorDiscoveredTypes = DiscoveredTypes.All;
        });
        services.AddJWTBearerAuth("TokenSigningKey");

        services.AddAuthentication();
        services.AddSwaggerDoc(settings =>
        {
            settings.Title = "My API";
            settings.Version = "v1";
        });

        return services;
    }
}
