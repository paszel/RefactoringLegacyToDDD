using System;

namespace Sales.Domain
{
  public class AlreadyExistsException : Exception
  {
    public AlreadyExistsException(string message) : base(message)
    {      
    }
  }
}