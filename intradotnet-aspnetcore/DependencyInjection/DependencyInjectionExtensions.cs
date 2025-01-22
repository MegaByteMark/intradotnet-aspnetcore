using IntraDotNet.AspNetCore.Middleware;
using Microsoft.AspNetCore.Builder;

namespace IntraDotNet.AspNetCore.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IApplicationBuilder UseWindowsImpersonation(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<WindowsImpersonationMiddleware>();

        return builder;
    }
}