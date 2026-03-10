using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Library.Model;

namespace QuantityMeasurementApp.Tests
{
  [TestClass]
  public class QuantityTest
  {
    [TestMethod]
    public void GivenFeetToFeet_SameValue_ShouldReturnTrue()
    {
      var q1 = new Quantity(1.0, LengthUnit.FEET);
      var q2 = new Quantity(1.0, LengthUnit.FEET);

      Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void GivenInchToInch_SameValue_ShouldReturnTrue()
    {
      var q1 = new Quantity(1.0, LengthUnit.INCH);
      var q2 = new Quantity(1.0, LengthUnit.INCH);

      Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void GivenFeetToInch_Equivalent_ShouldReturnTrue()
    {
      var q1 = new Quantity(1.0, LengthUnit.FEET);
      var q2 = new Quantity(12.0, LengthUnit.INCH);

      Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void GivenDifferentValues_ShouldReturnFalse()
    {
      var q1 = new Quantity(1.0, LengthUnit.FEET);
      var q2 = new Quantity(3.0, LengthUnit.YARD);

      Assert.IsFalse(q1.Equals(q2));
    }

    [TestMethod]
    public void GivenNull_ShouldReturnFalse()
    {
      var q1 = new Quantity(1.0, LengthUnit.FEET);

      Assert.IsFalse(q1.Equals(null));
    }

    [TestMethod]
    public void GivenSameReference_ShouldReturnTrue()
    {
      var q1 = new Quantity(1.0, LengthUnit.FEET);

      Assert.IsTrue(q1.Equals(q1));
    }

    [TestMethod]
    public void GivenFeetToInches_ShouldReturn12()
    {
      double result = Quantity.Convert(1.0, LengthUnit.FEET, LengthUnit.INCH);

      Assert.AreEqual(12.0, result, 0.0001);
    }

    [TestMethod]
    public void GivenFeetPlusInch_ShouldReturnFeet()
    {
      var q1 = new Quantity(1.0, LengthUnit.FEET);
      var q2 = new Quantity(12.0, LengthUnit.INCH);

      var result = q1.Add(q2);

      Assert.AreEqual(2.0, result.Value, 0.0001);
      Assert.AreEqual(LengthUnit.FEET, result.Unit);
    }

    [TestMethod]
    public void GivenFeetAndInch_TargetFeet_ShouldReturnFeet()
    {
      var q1 = new Quantity(1.0, LengthUnit.FEET);
      var q2 = new Quantity(12.0, LengthUnit.INCH);

      var result = q1.Add(q2, LengthUnit.FEET);

      Assert.AreEqual(2.0, result.Value, 0.0001);
      Assert.AreEqual(LengthUnit.FEET, result.Unit);
    }

    [TestMethod]
    public void GivenFeetAndInch_TargetInch_ShouldReturnInch()
    {
      var q1 = new Quantity(1.0, LengthUnit.FEET);
      var q2 = new Quantity(12.0, LengthUnit.INCH);

      var result = q1.Add(q2, LengthUnit.INCH);

      Assert.AreEqual(24.0, result.Value, 0.0001);
    }

    [TestMethod]
    public void GivenFeetAndInch_TargetYard_ShouldReturnYard()
    {
      var q1 = new Quantity(1.0, LengthUnit.FEET);
      var q2 = new Quantity(12.0, LengthUnit.INCH);

      var result = q1.Add(q2, LengthUnit.YARD);

      Assert.AreEqual(0.6667, result.Value, 0.0001);
    }
  }
}