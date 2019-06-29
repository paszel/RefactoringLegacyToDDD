using System;
using Sales.Domain;

namespace Sales.Infrastructure
{
  public class DateTimeProvider : IDateTimeProvider
  {
    public DateTime Today => DateTime.Today;
    public DateTime Now => DateTime.Now;
  }
}