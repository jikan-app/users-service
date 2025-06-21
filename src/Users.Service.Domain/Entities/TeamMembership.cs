using Users.Service.Domain.Enums;
using Users.Service.Domain.ValueObjects;

namespace Users.Service.Domain.Entities;

public class TeamMembership(TeamId teamId, Role role)
{
    public TeamId TeamId { get; private set; } = teamId;
    public Role Role { get; private set; } = role;
    public DateTime JoinedAt { get; private set; } = DateTime.UtcNow;
}
