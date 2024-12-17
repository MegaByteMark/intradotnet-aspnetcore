using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NSubstitute;
using System.Runtime.InteropServices;

namespace IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership.Tests
{
    public class WindowsGroupMembershipAuthorizationHandlerTest
    {
        [Fact]
        public async Task HandleRequirementAsync_UserInGroup_Succeeds()
        {
            //if the platform is not windows fail the test
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.True(false);
                return;
            }

            // Arrange
            var handler = new WindowsGroupMembershipAuthorizationHandler();
            var requirement = new WindowsGroupMembershipRequirement(["S-1-5-32-544"]); // Example SID for Administrators group
            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.GroupSid, "S-1-5-32-544")
            ], "mock"));

            var context = Substitute.For<AuthorizationHandlerContext>(
                new[] { requirement }, user, null);

            // Act
            await handler.HandleAsync(context);

            // Assert
            context.Received().Succeed(requirement);
        }

        [Fact]
        public async Task HandleRequirementAsync_UserNotInGroup_Fails()
        {
            //if the platform is not windows fail the test
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.True(false);
                return;
            }
            
            // Arrange
            var handler = new WindowsGroupMembershipAuthorizationHandler();
            var requirement = new WindowsGroupMembershipRequirement(["S-1-5-32-544"]); // Example SID for Administrators group
            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.GroupSid, "S-1-5-32-545") // Different group SID
            ], "mock"));

            var context = Substitute.For<AuthorizationHandlerContext>(
                new[] { requirement }, user, null);

            // Act
            await handler.HandleAsync(context);

            // Assert
            context.DidNotReceive().Succeed(requirement);
        }
    }
}