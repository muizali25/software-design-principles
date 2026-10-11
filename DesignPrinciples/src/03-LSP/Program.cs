using Bad = DesignPrinciples.LSP.Bad;
using Good = DesignPrinciples.LSP.Good;

var request = new Bad.RefundRequest("B-100", 25m);
Console.WriteLine($"Card refund: {new Bad.CardRefundProcessor().Refund(request):C}");
try { new Bad.StoreCreditRefundProcessor().Refund(request); }
catch (NotSupportedException exception) { Console.WriteLine($"Bad contract: {exception.Message}"); }

var result = new Good.RefundService(new Good.StoreCreditRefundPolicy())
    .Process(new Good.RefundRequest("G-100", 25m));
Console.WriteLine($"{result.Amount:C} returned as {result.Destination}");
