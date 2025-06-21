using Users.Service.Domain.Entities;
using Users.Service.Domain.Enums;
using Users.Service.Domain.ValueObjects;

namespace Users.Service.Domain.Tests;

public class TeamsTests
{
    [Fact]
    public void JoinTeam_Add_TeamMembership()
    {
        var user = CreateTestUser();
        var teamId = TeamId.New();

        user.JoinTeam(teamId, Role.Owner);

        Assert.Single(user.Teams);
        Assert.Contains(user.Teams, tm => tm.TeamId == teamId && tm.Role == Role.Owner);
    }

    [Fact]
    public void JoinTeam_Throw_IfAlreadyExists()
    {
        var user = CreateTestUser();
        var teamId = TeamId.New();

        user.JoinTeam(teamId, Role.Owner);

        Assert.Throws<InvalidOperationException>(() => user.JoinTeam(teamId, Role.Owner));
    }

    private static User CreateTestUser() =>
        new(UserId.New(), new Email("paulbuzakov@gmail.com"), "password-hash");
}
