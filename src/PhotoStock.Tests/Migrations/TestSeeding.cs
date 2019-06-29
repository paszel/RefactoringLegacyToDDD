using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace PhotoStock.Tests.Migrations
{
  [Migration(201901011000)]
  public class TestSeeding : FluentMigrator.ForwardOnlyMigration
  {
    public override void Up()
    {
      // Insert.IntoTable("Product").Row(new {Name = "Rysunek1", Price = 10});
    }
  }
}
