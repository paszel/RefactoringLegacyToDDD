using System;

namespace Sales.Application
{
  public class AlreadyExistsException : Exception
  {
    public AlreadyExistsException(string message) : base(message)
    {      
    }
  }
}