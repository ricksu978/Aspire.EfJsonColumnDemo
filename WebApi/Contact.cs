namespace WebApi;

public record Contact
{
    public Guid Id { get; init; }
    public required Address Address { get; init; }
}

public record Address(
    string Line1,
    string? Line2,
    string Suburb,
    string State,
    string PostCode
);
