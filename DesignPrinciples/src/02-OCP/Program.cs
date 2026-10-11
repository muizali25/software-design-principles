using Bad = DesignPrinciples.OCP.Bad;
using Good = DesignPrinciples.OCP.Good;

Console.WriteLine(new Bad.PaymentService().Process(new Bad.Payment(42m, Bad.PaymentMethod.CreditCard)));

var service = new Good.PaymentService([
    new Good.CreditCardProcessor(),
    new Good.PayPalProcessor(),
    new Good.BankTransferProcessor()]);
Console.WriteLine(service.Process("paypal", new Good.Payment(42m)));
