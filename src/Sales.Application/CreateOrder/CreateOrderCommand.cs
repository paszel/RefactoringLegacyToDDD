namespace Sales.Application.CreateOrder
{
  public class CreateOrderCommand
  {
    public CreateOrderCommand(string id, string clientId)
    {
      Id = id;
      ClientId = clientId;
    }

    public string ClientId { get;  }
    public string Id { get; }    
  }
}