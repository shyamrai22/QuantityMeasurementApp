using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Library.Model;

namespace QuantityMeasurementApp.Tests
{
  [TestClass]
  public class LengthUnitTests
  {
    [TestMethod]
    public void GivenFeet_ToBaseUnit_ShouldReturnSameValue()
    {
      double result = LengthUnit.FEET.ToBaseUnit(5);

      Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void GivenInch_ToBaseUnit_ShouldReturnFeet()
    {
      double result = LengthUnit.INCH.ToBaseUnit(12);

      Assert.AreEqual(1, result, 0.0001);
    }

    [TestMethod]
    public void GivenYard_ToBaseUnit_ShouldReturnFeet()
    {
      double result = LengthUnit.YARD.ToBaseUnit(1);

      Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void GivenCentimeter_ToBaseUnit_ShouldReturnFeet()
    {
      double result = LengthUnit.CENTIMETER.ToBaseUnit(30.48);

      Assert.AreEqual(1, result, 0.0001);
    }

    [TestMethod]
    public void GivenFeet_FromBaseUnit_ShouldReturnSameValue()
    {
      double result = LengthUnit.FEET.FromBaseUnit(2);

      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void GivenFeet_FromBaseUnit_ToInch_ShouldReturn12()
    {
      double result = LengthUnit.INCH.FromBaseUnit(1);

      Assert.AreEqual(12, result);
    }
  }
}