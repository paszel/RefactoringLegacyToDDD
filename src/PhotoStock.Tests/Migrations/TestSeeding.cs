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
    public static string ProductId = "P1";
    public static string Product2Id = "P2";
    public static string Product3Id = "P3";

    public override void Up()
    {
      Insert.IntoTable("Product").Row(new {Id = ProductId, Name = "Rysunek1", Price = 10});
      Insert.IntoTable("Product").Row(new { Id = Product2Id, Name = "Rysunek2", Price = 11 });
      Insert.IntoTable("Product").Row(new { Id = Product3Id, Name = "Rysunek3", Price = 4 });
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
