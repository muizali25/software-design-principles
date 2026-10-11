using Bad = DesignPrinciples.DIP.Bad;
using Good = DesignPrinciples.DIP.Good;

new Bad.OrderFulfillmentService().Fulfill(new Bad.FulfillmentOrder("B-100", "sam@example.com"));

var service = new Good.OrderFulfillmentService(
    new Good.ConsoleOrderRepository(),
    new Good.ConsoleEmailSender());
service.Fulfill(new Good.FulfillmentOrder("G-100", "sam@example.com"));
