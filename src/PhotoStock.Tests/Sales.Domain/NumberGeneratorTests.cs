using System;
using Moq;
using NUnit.Framework;
using Sales.Domain;

namespace PhotoStock.Tests.Sales.Domain
{
  [TestFixture]
  public class NumberGeneratorTests
  {
    private NumberGenerator _sut;
    private Mock<global::Sales.Domain.IDateTimeProvider> _dateTimeProviderMock;
    private Mock<IConfiguration> _configurationMock;

    [SetUp]
    public void SetUp()
    {
      _dateTimeProviderMock = new Mock<IDateTimeProvider>();
      _configurationMock = new Mock<IConfiguration>();
      _sut = new NumberGenerator(_configurationMock.Object, _dateTimeProviderMock.Object);
    }

    [Test]
    public void Genrate_should_return_nuber_with_demo_prefix()
    {
      // Setup
      _dateTimeProviderMock.Setup(f => f.Now).Returns(new DateTime(2020, 12, 24));
      _configurationMock.Setup(f => f["Environment"]).Returns("DEMO");

      // Act
      var result = _sut.GenerateNumber();

      //
      Assert.AreEqual("DEMO/Or/24.12.2020 00:00:00", result);
    }

    [Test]
    public void Genrate_should_return_nuber_without_prefix_for_prod_configuration()
    {
      // Setup
      _dateTimeProviderMock.Setup(f => f.Now).Returns(new DateTime(2020, 12, 24));
      _configurationMock.Setup(f => f["Environment"]).Returns("PROD");

      // Act
      var result = _sut.GenerateNumber();

      //
      Assert.AreEqual("Or/24.12.2020 00:00:00", result);
    }
  }
}