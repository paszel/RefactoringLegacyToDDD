namespace PhotoStock.Tests
{
  public class EmailData
  {
    public string From { get; }
    public string Recipients { get; }
    public string Subject { get; }
    public string Body { get; }

    public EmailData(string @from, string recipients, string subject, string body)
    {
      From = @from;
      Recipients = recipients;
      Subject = subject;
      Body = body;
    }
  }
}