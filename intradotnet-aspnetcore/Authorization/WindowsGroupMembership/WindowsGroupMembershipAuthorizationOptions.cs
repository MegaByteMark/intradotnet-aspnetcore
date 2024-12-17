namespace IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership
{
    /// <summary>
    /// Options for configuring Windows group membership authorization.
    /// </summary>
    public class WindowsGroupMembershipAuthorizationOptions
    {
        /// <summary>
        /// Gets or sets the list of policies for Windows group membership authorization.
        /// </summary>
        public required List<WindowsGroupMembershipAuthorizationPolicy> Policies { get; set; }
    }
}