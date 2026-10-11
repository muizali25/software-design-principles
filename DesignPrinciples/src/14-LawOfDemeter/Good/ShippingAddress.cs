namespace DesignPrinciples.LawOfDemeter.Good;

public sealed record ShippingAddress(string Street, string City, string PostalCode)
{
    public string Format() => $"{Street}, {City} {PostalCode}";
}
