using System;

namespace PhotoStock.Infrastructure
{
  class DateTimeProvider : IDateTimeProvider
  {
    public DateTime Now => DateTime.Now;

    public DateTime Today => DateTime.Today;
  }
}