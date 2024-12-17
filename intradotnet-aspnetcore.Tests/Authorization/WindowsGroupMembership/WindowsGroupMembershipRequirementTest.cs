using System.Collections.Generic;
using Xunit;
using IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership;

namespace IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership.Tests
{
    public class WindowsGroupMembershipRequirementTest
    {
        [Fact]
        public void Constructor_SetsGroupNames()
        {
            // Arrange
            var groupNames = new List<string> { "Administrators", "Users" };

            // Act
            var requirement = new WindowsGroupMembershipRequirement(groupNames);

            // Assert
            Assert.Equal(groupNames, requirement.GroupNames);
        }

        [Fact]
        public void Constructor_InitializesGroupSids()
        {
            // Arrange
            var groupNames = new List<string> { "Administrators", "Users" };

            // Act
            var requirement = new WindowsGroupMembershipRequirement(groupNames);

            // Assert
            Assert.NotNull(requirement.GroupSids);
            Assert.Empty(requirement.GroupSids);
        }

        [Fact]
        public void GroupSids_CanBeSet()
        {
            // Arrange
            var groupNames = new List<string> { "Administrators", "Users" };
            var requirement = new WindowsGroupMembershipRequirement(groupNames);
            var groupSids = new List<string> { "S-1-5-32-544", "S-1-5-32-545" };

            // Act
            requirement.GroupSids = groupSids;

            // Assert
            Assert.Equal(groupSids, requirement.GroupSids);
        }
    }
}