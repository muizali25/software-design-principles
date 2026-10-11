namespace DesignPrinciples.ISP.Bad;

public interface INotificationService
{
    void SendEmail(string address, string message);
    void SendSms(string phoneNumber, string message);
    void SendPush(string deviceId, string message);
    void SendFax(string number, string message);
}

public sealed class EmailNotificationService : INotificationService
{
    public void SendEmail(string address, string message) => Console.WriteLine($"Email to {address}: {message}");
    // PROBLEM: an email-only client must implement channels it cannot support.
    public void SendSms(string phoneNumber, string message) => throw new NotSupportedException();
    public void SendPush(string deviceId, string message) => throw new NotSupportedException();
    public void SendFax(string number, string message) => throw new NotSupportedException();
}
