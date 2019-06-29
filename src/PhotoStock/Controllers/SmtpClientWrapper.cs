using System.Net.Mail;

namespace PhotoStock.Controllers
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