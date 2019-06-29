using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Sales.Domain;
using Sales.Domain.Discount;
using Sales.Domain.Offer;

namespace PhotoStock.Tests.Sales.Domain.Discount
{
  [TestFixture]
  public class DiscountCalculatorTests
  {
    private DiscountCalculator _sut;
    private Mock<IDiscountSpecificationFactory> _discountSpecificationFactoryMock;

    [SetUp]
    public void SetUp()
    {
      _discountSpecificationFactoryMock = new Mock<IDiscountSpecificationFactory>();
      _sut = new DiscountCalculator(_discountSpecificationFactoryMock.Object);
    }

    [Test]
    public void Calculate_should_return_discount_when_specificatioin_return_true()
    {
      // Arrange
      _discountSpecificationFactoryMock.Setup(f => f.CreateGrassDaySpecyfication()).Returns(new TrueSpecification());
      _discountSpecificationFactoryMock.Setup(f => f.CreateHolidaySpecyfication()).Returns(new TrueSpecification());

      // Act
      var result = _sut.Calculate(new List<OfferItem>());

      //
      Assert.AreEqual(10,result);
    }
  }
}
