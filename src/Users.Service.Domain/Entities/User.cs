using Users.Service.Domain.Enums;
using Users.Service.Domain.ValueObjects;

namespace Users.Service.Domain.Entities;

public class User(UserId id, Email email, string passwordHash)
{
    public UserId Id { get; private set; } = id;
    public Email Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;
    public string? DisplayName { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public Subscription Subscription { get; private set; } = Subscription.CreateBase();

    private readonly List<TeamMembership> _teams = [];
    public IReadOnlyCollection<TeamMembership> Teams => _teams.AsReadOnly();

    public void UpdateDisplayName(string name) => DisplayName = name;

    public void UpgradeSubscription(SubscriptionTier newTier)
    {
        Subscription = Subscription.UpgradeForMonth(newTier);
    }

    public void JoinTeam(TeamId team, Role role)
    {
        var teamMembership = new TeamMembership(team, role);
        if (_teams.Any(t => t.TeamId == teamMembership.TeamId))
        {
            throw new InvalidOperationException("User is already a member of this team.");
        }

        _teams.Add(teamMembership);
    }
}
