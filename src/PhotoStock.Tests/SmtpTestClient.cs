using System.Collections.Generic;
using PhotoStock.Controllers;
using Process.OrderConfirmationProcess;

namespace PhotoStock.Tests
{
  public class SmtpTestClient : ISmtpClient
  {
    public List<EmailData> SentEmails = new List<EmailData>();
    public void Send(string @from, string recipients, string subject, string body)
    {
      SentEmails.Add(new EmailData(@from, recipients, subject, body));
    }
  }
}