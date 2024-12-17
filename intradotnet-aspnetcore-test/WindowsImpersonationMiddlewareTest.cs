using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;

namespace IntraDotNet.AspNetCore.Middleware.Tests
{
    public class WindowsImpersonationMiddlewareTest
    {
        [Fact]
        public async Task InvokeAsync_WithWindowsIdentity_RunsImpersonated()
        {
            //If the platform is not windows fail
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.True(false);
                return;
            }

            // Arrange
            var next = Substitute.For<RequestDelegate>();
            var middleware = new WindowsImpersonationMiddleware(next);

            var context = Substitute.For<HttpContext>();
            var user = Substitute.For<ClaimsPrincipal>();
            var windowsIdentity = Substitute.For<WindowsIdentity>("TestDomain\\TestUser");

            user.Identity.Returns(windowsIdentity);
            context.User.Returns(user);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            await next.Received(1).Invoke(context);
        }

        [Fact]
        public async Task InvokeAsync_WithoutWindowsIdentity_CallsNext()
        {
            //If the platform is not windows fail
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.True(false);
                return;
            }

            // Arrange
            var next = Substitute.For<RequestDelegate>();
            var middleware = new WindowsImpersonationMiddleware(next);

            var context = Substitute.For<HttpContext>();
            var user = Substitute.For<ClaimsPrincipal>();
            var identity = Substitute.For<IIdentity>();

            user.Identity.Returns(identity);
            context.User.Returns(user);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            await next.Received(1).Invoke(context);
        }

        [Fact]
        public async Task InvokeAsync_NullIdentity_CallsNext()
        {
            //If the platform is not windows fail
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.True(false);
                return;
            }

            // Arrange
            var next = Substitute.For<RequestDelegate>();
            var middleware = new WindowsImpersonationMiddleware(next);

            var context = Substitute.For<HttpContext>();
            var user = Substitute.For<ClaimsPrincipal>();

            user.Identity.Returns((IIdentity?)null);
            context.User.Returns(user);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            await next.Received(1).Invoke(context);
        }

        [Fact]
        public async Task InvokeAsync_NullUser_CallsNext()
        {
            //If the platform is not windows fail
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.True(false);
                return;
            }

            // Arrange
            var next = Substitute.For<RequestDelegate>();
            var middleware = new WindowsImpersonationMiddleware(next);

            var context = Substitute.For<HttpContext>();

            context.User.Returns((ClaimsPrincipal?)null);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            await next.Received(1).Invoke(context);
        }
    }
}