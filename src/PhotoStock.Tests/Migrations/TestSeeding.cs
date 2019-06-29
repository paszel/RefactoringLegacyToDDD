using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace PhotoStock.Tests.Migrations
{
  [Migration(201901011000)]
  public class TestSeeding : FluentMigrator.ForwardOnlyMigration
  {
    public static string ClientEmail = "ala.makotowska@example.com";

    public override void Up()
    {
      Insert.IntoTable("Product").Row(new {Name = "Rysunek1", Price = 10});
      Insert.IntoTable("Client").Row(new
      {
        Id = "aaa111",
        Name = "Ala makotowska",
        Credit = 100,
        CreditLeft = 100,
        InvoiceType = 1,
        Email = ClientEmail
      });
    }
  }
}
