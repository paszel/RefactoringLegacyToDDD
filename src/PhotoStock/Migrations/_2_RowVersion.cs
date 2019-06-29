using FluentMigrator;

namespace PhotoStock.Migrations
{
  [Migration(201902010810)]
  public class _2_RowVersion : ForwardOnlyMigration
  {
    public override void Up()
    {
      Execute.Sql("alter table [Order] add version int null");      
    }
  }
}