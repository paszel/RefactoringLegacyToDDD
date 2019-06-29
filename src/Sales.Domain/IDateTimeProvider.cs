using System;

namespace Sales.Domain
{
  public interface IDateTimeProvider
  {
    DateTime Today { get; }
    DateTime Now { get; }
  }
}