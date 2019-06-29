using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace PhotoStock.Infrastructure
{
  public class DbMigrations
  {
    public static void Run(string connectionString)
    {
      var assemblies = Directory.GetFiles(  
          AppDomain.CurrentDomain.BaseDirectory, "*.*")
        .Where(f => Path.GetExtension(f).ToLower() == ".dll" || Path.GetExtension(f).ToLower() == ".exe")
        .Where(f => Path.GetFileName(f).Contains("PhotoStock"))
        .Select(f => Assembly.LoadFile(f)).ToArray();

      ServiceCollection serviceCollection = new ServiceCollection();
      var sp = serviceCollection.AddFluentMigratorCore()
        .ConfigureRunner(rb =>
        {
          rb
            .AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(assemblies).For.Migrations();
        })
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);

      sp.GetService<IMigrationRunner>().MigrateUp();
    }
  }

}
