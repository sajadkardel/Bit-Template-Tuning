using BTT.App.Services.Implementations;
using BTT.Shared.Markers;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {

#if BlazorServer || BlazorHybrid
        services.AddScoped(sp =>
        {
            HttpClient httpClient = new(sp.GetRequiredService<AppHttpClientHandler>())
            {
                BaseAddress = new Uri($"{sp.GetRequiredService<IConfiguration>()["ApiServerAddress"]}api/")
            };

            return httpClient;
        });
#endif

        services.AddTransient<AppHttpClientHandler>();
        services.AddAuthorizationCore();
        services.AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>();
        services.AddScoped(sp => (AppAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());

        services.Scan(scan => scan.FromEntryAssembly()
        .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        services.Scan(scan => scan.FromEntryAssembly()
        .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
        .AsImplementedInterfaces()
        .WithSingletonLifetime());

        services.Scan(scan => scan.FromEntryAssembly()
        .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
        .AsImplementedInterfaces()
        .WithTransientLifetime());

        return services;
    }
}
