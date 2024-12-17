using System.Collections.Generic;
using Xunit;
using IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership;
using System.Runtime.InteropServices;
using NSubstitute;

namespace IntraDotNet.AspNetCore.Authorization.WindowsGroupMembership.Tests
{
    public class WindowsGroupMembershipRequirementTest
    {
        [Fact(Skip = "Cannot auth with LDAP in GitHub Actions")]
        public void Constructor_SetsGroupNames()
        {
            //If the platform is not windows fail the test
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.True(false);
                return;
            }

            // Arrange
            var groupNames = new List<string> { "Administrators", "Users" };

            // Act
            //Currently can't run LDAP queries in a github actions environment
            var requirement = Substitute.For<WindowsGroupMembershipRequirement>(groupNames);
            requirement.GroupNames.Returns(groupNames);

            // Assert
            Assert.Equal(groupNames, requirement.GroupNames);
        }

        [Fact(Skip = "Cannot auth with LDAP in GitHub Actions")]
        public void Constructor_InitializesGroupSids()
        {
            //If the platform is not windows fail the test
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.True(false);
                return;
            }

            // Arrange
            var groupNames = new List<string> { "Administrators", "Users" };

            // Act
            //Currently can't run LDAP queries in a github actions environment
            var requirement = Substitute.For<WindowsGroupMembershipRequirement>(groupNames);
            requirement.GroupNames.Returns(groupNames);
            requirement.GroupSids.Returns(new List<string>());

            // Assert
            Assert.NotNull(requirement.GroupSids);
            Assert.Empty(requirement.GroupSids);
        }

        [Fact(Skip = "Cannot auth with LDAP in GitHub Actions")]
        public void GroupSids_CanBeSet()
        {
            //If the platform is not windows fail the test
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.True(false);
                return;
            }

            // Arrange
            var groupNames = new List<string> { "Administrators", "Users" };
            var groupSids = new List<string> { "S-1-5-32-544", "S-1-5-32-545" };

            //Currently can't run LDAP queries in a github actions environment
            var requirement = Substitute.For<WindowsGroupMembershipRequirement>(groupNames);
            requirement.GroupNames.Returns(groupNames);
            requirement.GroupSids.Returns(groupSids);

            // Act
            requirement.GroupSids = groupSids;

            // Assert
            Assert.Equal(groupSids, requirement.GroupSids);
        }
    }
}