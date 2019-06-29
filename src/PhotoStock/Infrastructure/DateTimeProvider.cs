using System;

namespace PhotoStock.Infrastructure
{
  class DateTimeProvider : IDateTimeProvider
  {
    public DateTime Now()
    {
      return DateTime.Now;
    }
  }
}