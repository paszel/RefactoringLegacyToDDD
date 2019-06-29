using System;

namespace PhotoStock.Infrastructure
{
  public interface IDateTimeProvider
  {
    DateTime Now();
  }
}