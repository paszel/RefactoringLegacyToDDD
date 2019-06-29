using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Sales.Domain;
using Sales.Domain.Discount;

namespace PhotoStock.Tests.Sales.Domain.Discount
{
  [TestFixture]
  public class DateRangeSpecificationTest
  {
    [Test]
    public void IsSatisfied_should_return_true_when_date_in_range()
    {
      // Arrange
      DateRangeSpecification spec = new DateRangeSpecification(1,1,2,1,new DateTime(1999,1,1));

      // Act, Assert
      Assert.IsTrue(spec.IsSatisfiedBy(new List<OfferItem>()));
    }
  }
}
