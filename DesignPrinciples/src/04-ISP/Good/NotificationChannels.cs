namespace DesignPrinciples.ISP.Good;

public interface IEmailSender { void SendEmail(string address, string message); }
public interface ISmsSender { void SendSms(string phoneNumber, string message); }
public interface IPushSender { void SendPush(string deviceId, string message); }
public interface IFaxSender { void SendFax(string number, string message); }

public sealed class EmailSender : IEmailSender
{
    public List<string> SentMessages { get; } = [];
    public void SendEmail(string address, string message) => SentMessages.Add($"{address}: {message}");
}

public sealed class OrderReceiptNotifier(IEmailSender emailSender)
{
    public void Notify(string email, string receipt) => emailSender.SendEmail(email, receipt);
}

public sealed class DeliveryUpdateNotifier(ISmsSender smsSender, IPushSender pushSender)
{
    public void Notify(string phoneNumber, string deviceId, string message)
    {
        smsSender.SendSms(phoneNumber, message);
        pushSender.SendPush(deviceId, message);
    }
}
