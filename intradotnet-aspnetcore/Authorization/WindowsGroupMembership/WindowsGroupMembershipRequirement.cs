using System.DirectoryServices.AccountManagement;
using Microsoft.AspNetCore.Authorization;

namespace IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership
{
    /// <summary>
    /// Represents a requirement for Windows group membership authorization.
    /// </summary>
    public class WindowsGroupMembershipRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Gets the names of the Windows groups required for authorization.
        /// </summary>
        public IEnumerable<string> GroupNames { get; }

        /// <summary>
        /// Gets or sets the SIDs of the Windows groups required for authorization.
        /// </summary>
        public IEnumerable<string> GroupSids { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsGroupMembershipRequirement"/> class.
        /// </summary>
        /// <param name="groupNames">The names of the Windows groups required for authorization.</param>
        public WindowsGroupMembershipRequirement(IEnumerable<string> groupNames)
        {
            GroupNames = groupNames;
            GroupSids = new List<string>();

            translateGroupNamesToSids();
        }

        /// <summary>
        /// Translates the group names to SIDs.
        /// </summary>
        private void translateGroupNamesToSids()
        {
            List<string> sids = new List<string>();

#pragma warning disable CA1416 // Validate platform compatibility
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                foreach (string groupName in GroupNames)
                {
                    var sid = GetSidFromGroupName(groupName, context);
                    if (sid != null)
                    {
                        sids.Add(sid);
                    }
                }
            }
#pragma warning restore CA1416

            GroupSids = sids;
        }

        /// <summary>
        /// Gets the SID from the group name.
        /// </summary>
        /// <param name="groupName">The name of the group.</param>
        /// <param name="context">The principal context.</param>
        /// <returns>The SID of the group.</returns>
        private string GetSidFromGroupName(string groupName, PrincipalContext context)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            using (GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupName))
            {
                return group?.Sid?.ToString()!;
            }
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}