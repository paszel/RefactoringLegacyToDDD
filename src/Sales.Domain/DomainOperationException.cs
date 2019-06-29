using System;

namespace Sales.Domain
{
  public class DomainOperationException : Exception
  {
    public string AggregateId { get; set; }

    public DomainOperationException(string aggregateId, string message) : base(message)
    {
      AggregateId = aggregateId;      
    }
  }
}