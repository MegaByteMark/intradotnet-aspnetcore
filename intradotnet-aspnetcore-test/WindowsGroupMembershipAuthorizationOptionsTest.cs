namespace IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership.Tests
{
    public class WindowsGroupMembershipAuthorizationOptionsTest
    {
        [Fact]
        public void AddGroup_AddsGroupToList()
        {
            // Arrange
            var groupSid = "S-1-5-32-544"; // Example SID for Administrators group

            var options = new WindowsGroupMembershipAuthorizationOptions()
            {
                Policies = [
                    new WindowsGroupMembershipAuthorizationPolicy()
                    {
                        Name = "Test",
                        AllowedGroups = [groupSid]
                    }
                ]
            };

            // Act

            // Assert
            Assert.Contains(groupSid, options.Policies[0].AllowedGroups);
        }

        [Fact]
        public void RemoveGroup_RemovesGroupFromList()
        {
            // Arrange
            var groupSid = "S-1-5-32-544"; // Example SID for Administrators group

            var options = new WindowsGroupMembershipAuthorizationOptions()
            {
                Policies = [
                    new WindowsGroupMembershipAuthorizationPolicy()
                    {
                        Name = "Test",
                        AllowedGroups = [groupSid]
                    }
                ]
            };

            // Act
            options.Policies[0].AllowedGroups.Remove(groupSid);

            // Assert
            Assert.DoesNotContain(groupSid, options.Policies[0].AllowedGroups);
        }

        [Fact]
        public void ClearGroups_ClearsAllGroups()
        {
            // Arrange
            var groupSid = "S-1-5-32-544"; // Example SID for Administrators group

            var options = new WindowsGroupMembershipAuthorizationOptions()
            {
                Policies = [
                    new WindowsGroupMembershipAuthorizationPolicy()
                    {
                        Name = "Test",
                        AllowedGroups = [groupSid, "S-1-5-32-545"]
                    }
                ]
            };

            // Act
            options.Policies[0].AllowedGroups.Clear();

            // Assert
            Assert.Empty(options.Policies[0].AllowedGroups);
        }
    }
}