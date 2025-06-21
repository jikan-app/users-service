using Users.Service.Domain.Entities;
using Users.Service.Domain.Enums;
using Users.Service.Domain.ValueObjects;

namespace Users.Service.Domain.Tests;

public class UsersTests
{
    [Fact]
    public void CreateUser_Have_BaseSubscription()
    {
        var user = CreateTestUser();

        Assert.Equal(SubscriptionTier.Base, user.Subscription.Tier);
        Assert.Null(user.Subscription.ExpiresAt);
    }

    [Fact]
    public void CreateUser_Have_ProSubscription()
    {
        var user = CreateTestUser();
        user.UpgradeSubscription(SubscriptionTier.Pro);

        Assert.Equal(SubscriptionTier.Pro, user.Subscription.Tier);
        Assert.NotNull(user.Subscription.ExpiresAt);

        var totalDays = user.Subscription.ExpiresAt!.Value.Subtract(DateTime.UtcNow).TotalDays;
        Assert.InRange(totalDays, 29, 31);
    }

    [Fact]
    public void CreateUser_Have_EnterpriseSubscription()
    {
        var user = CreateTestUser();
        user.UpgradeSubscription(SubscriptionTier.Enterprise);

        Assert.Equal(SubscriptionTier.Enterprise, user.Subscription.Tier);
        Assert.NotNull(user.Subscription.ExpiresAt);

        var totalDays = user.Subscription.ExpiresAt!.Value.Subtract(DateTime.UtcNow).TotalDays;
        Assert.InRange(totalDays, 29, 31);
    }

    [Fact]
    public void UpdateDisplayName_Change_DisplayName()
    {
        var user = CreateTestUser();
        user.UpdateDisplayName("Paul Buzakov");

        Assert.Equal("Paul Buzakov", user.DisplayName);
    }

    private static User CreateTestUser() =>
        new(UserId.New(), new Email("paulbuzakov@gmail.com"), "password-hash");
}
