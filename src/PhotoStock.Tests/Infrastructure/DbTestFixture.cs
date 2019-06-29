using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;

namespace PhotoStock.Tests.Infrastructure
{
  public class DbTest 
  {
    public static string CreateEmptyDatabase()
    {
      string connectionString = "Server=(local);Database=Test;Integrated Security=true;";
    
      const int DatabaseExistsButMdfFileIsMissingErrorCode = 5120;

      var builder = new DbConnectionStringBuilder { ConnectionString = connectionString };

      builder.Remove("Database");

      using (var connection = new SqlConnection(builder.ConnectionString))
      {
        void ExecuteSql(string x)
        {
          new SqlCommand(x, connection).ExecuteNonQuery();
        }

        connection.Open();

        try
        {
          ExecuteSql(@"
            IF EXISTS(SELECT NULL FROM sys.databases WHERE Name='Test')
              ALTER DATABASE Test SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
        }
        catch (SqlException ex)
        {
          if (ex.Number == DatabaseExistsButMdfFileIsMissingErrorCode)
          {
            ExecuteSql(@"DROP DATABASE Test;");
          }
          else
          {
            throw;
          }
        }

        string[] files = connection.Query<string>(@"                    
            IF( EXISTS(SELECT NULL FROM sys.databases WHERE Name='Test'))
            begin
				      declare @sql nvarchar(64)
				      set @sql = 'use [Test];SELECT physical_name FROM sys.database_files'
				      EXECUTE sp_executesql @sql
              SELECT physical_name FROM sys.database_files
              use [master]
              EXEC sp_detach_db 'Test';                            
            end").ToArray();

        if (files.Length > 0)
        {
          foreach (string file in files)
          {
            File.Delete(file);
          }
        }

        ExecuteSql($@"
            CREATE DATABASE [Test]
                ON PRIMARY (NAME=Test_data, FILENAME = '{Path.GetTempPath() + Guid.NewGuid()}')
                LOG ON (NAME=Test_log, FILENAME = '{Path.GetTempPath() + Guid.NewGuid()}')");

        ExecuteSql(@"ALTER DATABASE Test SET MULTI_USER WITH ROLLBACK IMMEDIATE;");
      }      

      return connectionString;
    }
  }
}
