namespace DesignPrinciples.OCP.Bad;

public enum PaymentMethod { CreditCard, PayPal, BankTransfer }
public sealed record Payment(decimal Amount, PaymentMethod Method);

public sealed class PaymentService
{
    public string Process(Payment payment)
    {
        // PROBLEM: every new payment method requires modifying this switch.
        return payment.Method switch
        {
            PaymentMethod.CreditCard => $"Charged card {payment.Amount:C}",
            PaymentMethod.PayPal => $"Charged PayPal {payment.Amount:C}",
            PaymentMethod.BankTransfer => $"Initiated bank transfer {payment.Amount:C}",
            _ => throw new ArgumentOutOfRangeException(nameof(payment))
        };
    }
}
