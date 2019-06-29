namespace PhotoStock.Controllers
{
  public interface ISmtpClient
  {
    void Send(string from, string recipients, string subject, string body);
  }
}