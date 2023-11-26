namespace sms;

public class Notification
{
    private readonly ISmsService[] _smsSenders;

    public Notification()
    {
        _smsSenders = new ISmsService[]
        {
            new GrameenSmsSender(),
            new RobiSmsSender(),
            new BanglaLinkSmsSender()
        };

        _smsSenders = _smsSenders.OrderBy(s => s.Priority).ToArray();
    }

    public Notification(ISmsService[] smsSenders)
    {
        _smsSenders = smsSenders;
    }

    public async Task<SmsResponse> SendSmsAsync(string text, string phoneNumber)
    {
        var retryCount = 0;
        SmsResponse response = null;

        while (retryCount < 3)
        {
            foreach (var smsSender in _smsSenders)
            {
                try
                {
                    response = smsSender.SendSms(text, phoneNumber);

                    if (response.Status == 200) return response;
                }
                catch (Exception ex)
                {
                    // Log the exception here
                    Console.WriteLine($"Error sending SMS: {ex.Message}");
                }
            }

            retryCount++;
            await Task.Delay(1000);
        }

        return new SmsResponse { Status = 500, Response = "Failed to send SMS" };
    }
}