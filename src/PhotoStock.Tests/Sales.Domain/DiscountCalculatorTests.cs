using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Sales.Domain;

namespace PhotoStock.Tests.Sales.Domain
{
  [TestFixture]
  public class DiscountCalculatorTests
  {
    private DiscountCalculator _sut;
    private Mock<IDateTimeProvider> _dateTimeProviderMock;

    [SetUp]
    public void SetUp()
    {
      _dateTimeProviderMock = new Mock<IDateTimeProvider>();
      _sut = new DiscountCalculator(_dateTimeProviderMock.Object);
    }

    [Test]
    public void Calculate_should_return_discount_when_today_is_24_12()
    {
      // Setup
      IEnumerable<OfferItem> availabeItems = new OfferItem[]{ new OfferItem(){ Id = "1", Name = "test", Price = 10, ProductType = global::Sales.Domain.ProductType.Printed} };
      _dateTimeProviderMock.Setup(f => f.Today).Returns(new DateTime(2020, 12, 24));

      // Act
      var result = _sut.Calculate(availabeItems);

      //
      Assert.AreEqual(10,result);
    }
  }
}
