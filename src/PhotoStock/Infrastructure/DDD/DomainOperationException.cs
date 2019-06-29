using System;

namespace PhotoStock.Infrastructure.DDD
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