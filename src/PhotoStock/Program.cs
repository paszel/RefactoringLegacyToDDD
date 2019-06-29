using System;

namespace PhotoStock
{
  public class Program
  {
    public static void Main(string[] args)
    {
      try
      {
        Bootstrap.Run(args);
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
      }

      Console.ReadKey();

    }
  }
}
