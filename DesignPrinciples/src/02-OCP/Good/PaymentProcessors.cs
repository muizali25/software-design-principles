namespace DesignPrinciples.OCP.Good;

public sealed record Payment(decimal Amount);
public interface IPaymentProcessor
{
    string Method { get; }
    string Process(Payment payment);
}

public sealed class CreditCardProcessor : IPaymentProcessor
{
    public string Method => "card";
    public string Process(Payment payment) => $"Charged card {payment.Amount:C}";
}

public sealed class PayPalProcessor : IPaymentProcessor
{
    public string Method => "paypal";
    public string Process(Payment payment) => $"Charged PayPal {payment.Amount:C}";
}

public sealed class BankTransferProcessor : IPaymentProcessor
{
    public string Method => "bank";
    public string Process(Payment payment) => $"Initiated bank transfer {payment.Amount:C}";
}

public sealed class PaymentService(IEnumerable<IPaymentProcessor> processors)
{
    private readonly Dictionary<string, IPaymentProcessor> _processors =
        processors.ToDictionary(processor => processor.Method, StringComparer.OrdinalIgnoreCase);

    public string Process(string method, Payment payment) =>
        _processors.TryGetValue(method, out var processor)
            ? processor.Process(payment)
            : throw new NotSupportedException($"Payment method '{method}' is not supported.");
}
