using Users.Service.Domain.Enums;

namespace Users.Service.Domain.Entities;

public class Subscription
{
    public SubscriptionTier Tier { get; private set; }
    public DateTime? ExpiresAt { get; private set; }

    public static Subscription CreateBase() =>
        new() { Tier = SubscriptionTier.Base, ExpiresAt = null };

    public Subscription UpgradeForMonth(SubscriptionTier newTier) =>
        new() { Tier = newTier, ExpiresAt = DateTime.UtcNow.AddMonths(1) };
}
