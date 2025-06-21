namespace Users.Service.Domain.ValueObjects;

public readonly record struct TeamId(Guid Value)
{
    public static TeamId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}
