namespace Users.Service.Domain.ValueObjects;

public record Email
{
    public string Value { get; private set; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(value));
        }

        if (!IsValidEmail(value))
        {
            throw new ArgumentException("Invalid email format.", nameof(value));
        }

        Value = value;
    }

    private bool IsValidEmail(string value)
    {
        return value.Contains('@') && value.Contains('.');
    }

    public static implicit operator string(Email email) => email.Value;
}
