using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnitTestProject1;
using PhotoStock.Infrastructure;
using PhotoStock.Tests.Contract;
using PhotoStock.Tests.Infrastructure;
using RestEase;

namespace PhotoStock.Tests
{
  [TestFixture]
  public class OrderTests 
  {
    private static readonly string _url = "http://localhost:5000";
    private readonly IApi _api = RestClient.For<IApi>(_url);

    [OneTimeSetUp]
    public void Setup()
    {
      //string connectionString = DbTest.CreateEmptyDatabase();

      //DbMigrations.Run(connectionString);

      Bootstrap.Run(new string[0]);

      var c = RestClient.For<IApi>(_url);

      
    }
  }
}