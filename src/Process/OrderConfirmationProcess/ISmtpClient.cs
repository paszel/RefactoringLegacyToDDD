namespace Process.OrderConfirmationProcess
{
  public interface ISmtpClient
  {
    void Send(string from, string recipients, string subject, string body);
  }
}