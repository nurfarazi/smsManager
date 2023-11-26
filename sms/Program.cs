using sms;

var notification = new Notification();
var response = await notification.SendSmsAsync("Hello World", "01700000000");

Console.WriteLine($"Response: {response.Response}");

public enum PriorityEnum
{
    Grameen = 0,
    BanglaLink = 1,
    Robi = 2
}

public class BanglaLinkSmsSender : ISmsService
{
    public int Priority { get; } = PriorityEnum.BanglaLink.GetHashCode();
    public SmsResponse SendSms(string text, string phoneNumber)
    {
        Console.WriteLine($"Sending sms with BanglaLink to {phoneNumber}: {text}");
        return new SmsResponse { Status = 200, Response = "ok" };
    }
}

public class GrameenSmsSender : ISmsService
{
    public int Priority { get; } = PriorityEnum.Grameen.GetHashCode();
    public SmsResponse SendSms(string text, string phoneNumber)
    {
        Console.WriteLine($"Sending sms with Grameen to {phoneNumber}: {text}");
        return new SmsResponse { Status = 200, Response = "ok" };
    }
}

public class RobiSmsSender : ISmsService
{
    public int Priority { get; } = PriorityEnum.Robi.GetHashCode();

    public SmsResponse SendSms(string text, string phoneNumber)
    {
        Console.WriteLine($"Sending sms with Robi to {phoneNumber}: {text}");
        return new SmsResponse { Status = 200, Response = "ok" };
    }
}

public interface ISmsService
{
    int Priority { get; }
    SmsResponse SendSms(string text, string phoneNumber);
}
public class SmsResponse
{
    public int Status { get; set; }
    public string Response { get; set; }
}