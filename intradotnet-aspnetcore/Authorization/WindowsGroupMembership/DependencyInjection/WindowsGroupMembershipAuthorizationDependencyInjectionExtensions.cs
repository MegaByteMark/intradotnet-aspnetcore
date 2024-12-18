using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership.DependencyInjection;

/// <summary>
/// Extension methods for setting up Windows group membership authorization services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class WindowsGroupMembershipAuthorizationDependencyInjectionExtensions
{
    /// <summary>
    /// Adds Windows group membership authorization services to the specified <see cref="IServiceCollection"/>.
    /// You must ensure that the configuration contains Authorization:WindowsGroupMembershipAuthorization. See the example projects for more details.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configureOptions"> A custom function to run to setup the <see cref="WindowsGroupMembershipAuthorizationOptions"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddWindowsGroupMembershipAuthorization(this IServiceCollection services, Func<WindowsGroupMembershipAuthorizationOptions> configureOptions)
    {
        WindowsGroupMembershipAuthorizationOptions options = configureOptions();

        // Add the authorization handler to the service collection
        services.AddSingleton<IAuthorizationHandler, WindowsGroupMembershipAuthorizationHandler>();

        if(options != null && options.Policies != null)
        {
            // Add the authorization policies to the service collection
            services.AddAuthorizationCore(configure =>
            {
                foreach (WindowsGroupMembershipAuthorizationPolicy policy in options.Policies)
                {
                    // Add the policy to the authorization configuration
                    configure.AddPolicy(policy.Name, (p) =>
                    {
                        p.Requirements.Add(new WindowsGroupMembershipRequirement(policy.AllowedGroups));
                    });
                }
            });
        }

        return services;
    }
}