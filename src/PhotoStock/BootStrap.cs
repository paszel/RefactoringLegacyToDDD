using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace PhotoStock
{
  public class Bootstrap
  {
    public static void Run(string[] args, /*Action<ContainerBuilder> overrideDependencies = null, */ string connectionString = null)
    {
      //if (overrideDependencies != null)
      //{
      //  Startup.RegisterExternalTypes = overrideDependencies;
      //}

      KeyValuePair<string, string> kv = new KeyValuePair<string, string>("ConnectionString", connectionString);
      var c = WebHost.CreateDefaultBuilder(args)
        .UseKestrel()
        .UseStartup<Startup>();
      if (connectionString != null)
      {
        c.ConfigureAppConfiguration(conf => conf.AddInMemoryCollection(new[] { kv }));
      }


      ThreadPool.QueueUserWorkItem(state => { c.Build().Run(); });
      Thread.Sleep(100);
    }
  }

}
