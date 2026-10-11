using DesignPrinciples.ISP.Good;

namespace DesignPrinciples.ISP.Tests;

public sealed class NotificationChannelTests
{
    [Fact]
    public void Receipt_Notifier_Depends_Only_On_Email()
    {
        var sender = new FakeEmailSender();

        new OrderReceiptNotifier(sender).Notify("sam@example.com", "Receipt #1");

        Assert.Equal("sam@example.com", sender.Address);
        Assert.Equal("Receipt #1", sender.Message);
    }

    [Fact]
    public void Delivery_Update_Notifier_Uses_Sms_And_Push_Without_Email_Or_Fax()
    {
        var sms = new FakeSmsSender();
        var push = new FakePushSender();

        new DeliveryUpdateNotifier(sms, push).Notify("+15551212", "device-9", "Out for delivery");

        Assert.Equal("+15551212", sms.PhoneNumber);
        Assert.Equal("device-9", push.DeviceId);
        Assert.Equal("Out for delivery", sms.Message);
        Assert.Equal("Out for delivery", push.Message);
    }

    private sealed class FakeEmailSender : IEmailSender
    {
        public string? Address { get; private set; }
        public string? Message { get; private set; }
        public void SendEmail(string address, string message) => (Address, Message) = (address, message);
    }

    private sealed class FakeSmsSender : ISmsSender
    {
        public string? PhoneNumber { get; private set; }
        public string? Message { get; private set; }
        public void SendSms(string phoneNumber, string message) => (PhoneNumber, Message) = (phoneNumber, message);
    }

    private sealed class FakePushSender : IPushSender
    {
        public string? DeviceId { get; private set; }
        public string? Message { get; private set; }
        public void SendPush(string deviceId, string message) => (DeviceId, Message) = (deviceId, message);
    }
}
