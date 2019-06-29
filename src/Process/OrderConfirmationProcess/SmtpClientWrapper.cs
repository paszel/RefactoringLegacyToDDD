using System.Net.Mail;

namespace Process.OrderConfirmationProcess
{
  class SmtpClientWrapper : ISmtpClient
  {
    public void Send(string @from, string recipients, string subject, string body)
    {
      SmtpClient c = new SmtpClient("smtp.photostock.com");
      c.Send(@from, recipients, subject, body);
    }
  }
}