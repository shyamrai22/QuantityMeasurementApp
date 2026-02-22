using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

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
  }
}