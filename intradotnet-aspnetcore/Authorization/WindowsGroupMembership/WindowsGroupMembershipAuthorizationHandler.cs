using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership
{
    /// <summary>
    /// Authorization handler that checks if a user is a member of a specified Windows group.
    /// </summary>
    public class WindowsGroupMembershipAuthorizationHandler : AuthorizationHandler<WindowsGroupMembershipRequirement>
    {
        /// <summary>
        /// Makes a decision if authorization is allowed based on the user's group membership.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns>A task that represents the completion of the operation.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WindowsGroupMembershipRequirement requirement)
        {
            string groupSid;
            IEnumerable<Claim> groupClaims;

            if (context.User.Identity is ClaimsIdentity identity && context.User.Identity.IsAuthenticated)
            {
                groupClaims = identity.FindAll(ClaimTypes.GroupSid);

                foreach (var claim in groupClaims)
                {
                    groupSid = claim.Value;

                    if (requirement.GroupSids.Any(g => g.Equals(groupSid, StringComparison.OrdinalIgnoreCase)))
                    {
                        context.Succeed(requirement);
                        break;
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}