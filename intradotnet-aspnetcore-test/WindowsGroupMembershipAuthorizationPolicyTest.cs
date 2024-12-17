namespace IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership.Tests
{
    public class WindowsGroupMembershipAuthorizationPolicyTest
    {
        [Fact]
        public void Policy_HasNameAndAllowedGroups()
        {
            // Arrange
            var policyName = "TestPolicy";
            var allowedGroups = new List<string> { "S-1-5-32-544", "S-1-5-32-545" };

            // Act
            var policy = new WindowsGroupMembershipAuthorizationPolicy
            {
                Name = policyName,
                AllowedGroups = allowedGroups
            };

            // Assert
            Assert.Equal(policyName, policy.Name);
            Assert.Equal(allowedGroups, policy.AllowedGroups);
        }

        [Fact]
        public void Policy_AllowedGroupsCanBeModified()
        {
            // Arrange
            var policy = new WindowsGroupMembershipAuthorizationPolicy
            {
                Name = "TestPolicy",
                AllowedGroups = new List<string> { "S-1-5-32-544" }
            };

            // Act
            policy.AllowedGroups.Add("S-1-5-32-545");

            // Assert
            Assert.Contains("S-1-5-32-545", policy.AllowedGroups);
        }
    }
}