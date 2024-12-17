namespace IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership
{
    /// <summary>
    /// Represents a policy for Windows group membership authorization.
    /// </summary>
    public class WindowsGroupMembershipAuthorizationPolicy
    {
        /// <summary>
        /// Gets or sets the name of the policy.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of allowed Windows groups for this policy.
        /// </summary>
        public required List<string> AllowedGroups { get; set; }
    }
}