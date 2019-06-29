using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using PhotoStock.Controllers;
using PhotoStock.Infrastructure;

namespace PhotoStock.Tests.Controllers
{
  [TestFixture]
  public class DiscountCalculatorTests
  {
    private DiscountCalculator _sut;
    
    [SetUp]
    public void SetUp()
    {
      _sut = new DiscountCalculator();
    }

    [Test]
    public void Calculate_should_return_discount_when_today_is_24_12()
    {
      // Setup
      IEnumerable<OfferItemDto> availabeItems = new OfferItemDto[]{ new OfferItemDto(){ Id = "1", Name = "test", Price = 10, ProductType = ProductType.Printed} };

      // Act
      var result = _sut.Calculate(new DateTime(2020, 12, 24),availabeItems);

      //
      Assert.AreEqual(10,result);
    }
  }

  [TestFixture]
  public class NumberGeneratorTests
  {
    private NumberGenerator _sut;
    private Mock<IDateTimeProvider> _dateTimeProviderMock;
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
