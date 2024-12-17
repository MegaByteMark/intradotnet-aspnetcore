using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace IntraDotNet.Windows.Server.Authorization.WindowsGroupMembership.DependencyInjection;

/// <summary>
/// Extension methods for setting up Windows group membership authorization services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class WindowsGroupMembershipAuthorizationDependencyInjectionExtensions
{
    /// <summary>
    /// Adds Windows group membership authorization services to the specified <see cref="IServiceCollection"/>.
    /// This method reads the configuration settings for the application and configures the authorization policies, requirements, and handlers.
    /// Before calling this method, the configuration settings must be loaded into the <see cref="IConfiguration"/> object.
    /// You must ensure that the configuration contains Authorization:WindowsGroupMembershipAuthorization. See the example projects for more details.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddWindowsGroupMembershipAuthorization(this IServiceCollection services)
    {
        // Add the authorization handler to the service collection
        services.AddSingleton<IAuthorizationHandler, WindowsGroupMembershipAuthorizationHandler>();

        // Configure the options for the Windows group membership authorization services
        services.PostConfigure<WindowsGroupMembershipAuthorizationOptions>(options =>
        {
            // Bind the configuration settings to the options
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
        });

        return services;
    }
}