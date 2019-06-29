using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Autofac;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace PhotoStock
{
  public class Bootstrap
  {
    public static void Run(string[] args, Action<ContainerBuilder> overrideDependencies = null, string connectionString = null)
    {
      if (overrideDependencies != null)
      {
        Startup.RegisterExternalTypes = overrideDependencies;
      }

      KeyValuePair<string, string> kv = new KeyValuePair<string, string>("ConnectionString", connectionString);
      var c = WebHost.CreateDefaultBuilder(args)
        .UseKestrel()
        .UseStartup<Startup>();
      if (connectionString != null)
      {
        c.ConfigureAppConfiguration(conf => conf.AddInMemoryCollection(new[] { kv }));
      }


      ThreadPool.QueueUserWorkItem(state => { c.Build().Run(); });

      var client = new HttpClient();

      for (int i = 0; i < 10; i++)
      {
        try
        {
          client.GetStringAsync("http://localhost:5000/api/products").Wait();
          break;
        }
        catch (Exception)
        {
          Console.WriteLine($"Connecting ({i}) ...");
          if (i == 9)
          {
            throw new Exception("Giving up");
          }
        }
      }
    }
  }

}
