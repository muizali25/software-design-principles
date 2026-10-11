using Bad = DesignPrinciples.ISP.Bad;
using Good = DesignPrinciples.ISP.Good;

var bad = new Bad.EmailNotificationService();
bad.SendEmail("sam@example.com", "Order received");
try { bad.SendSms("555-0100", "Order received"); }
catch (NotSupportedException) { Console.WriteLine("Bad email sender cannot send SMS."); }

var emailSender = new Good.EmailSender();
new Good.OrderReceiptNotifier(emailSender).Notify("sam@example.com", "Order received");
Console.WriteLine(emailSender.SentMessages.Single());
