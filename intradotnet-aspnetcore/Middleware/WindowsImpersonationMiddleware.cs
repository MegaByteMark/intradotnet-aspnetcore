namespace IntraDotNet.AspNetCore.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System.Security.Principal;
    using System.Threading.Tasks;

    /// <summary>
    /// Middleware to handle Windows impersonation.
    /// </summary>
    public class WindowsImpersonationMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsImpersonationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        public WindowsImpersonationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware with the given context.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task that represents the completion of request processing.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity is WindowsIdentity windowsIdentity)
            {
                // Suppress CA1416 warning
                #pragma warning disable CA1416
                await WindowsIdentity.RunImpersonated(windowsIdentity.AccessToken, async () => await _next(context));
                #pragma warning restore CA1416
            }
            else
            {
                await _next(context);
            }
        }
    }
}