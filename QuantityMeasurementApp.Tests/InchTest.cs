using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Tests
{
  [TestClass]
  public class InchTest
  {
    [TestMethod]
    public void GivenSameValue_WhenCompared_ShouldReturnTrue()
    {
      Inch i1 = new Inch(1.0);
      Inch i2 = new Inch(1.0);

      Assert.IsTrue(i1.Equals(i2));
    }

    [TestMethod]
    public void GivenDifferentValues_WhenCompared_ShouldReturnFalse()
    {
      Inch i1 = new Inch(1.0);
      Inch i2 = new Inch(2.0);

      Assert.IsFalse(i1.Equals(i2));
    }

    [TestMethod]
    public void GivenNull_WhenCompared_ShouldReturnFalse()
    {
      Inch i1 = new Inch(1.0);

      Assert.IsFalse(i1.Equals(null));
    }

    [TestMethod]
    public void GivenSameReference_WhenCompared_ShouldReturnTrue()
    {
      Inch i1 = new Inch(1.0);

      Assert.IsTrue(i1.Equals(i1));
    }

    [TestMethod]
    public void GivenDifferentType_WhenCompared_ShouldReturnFalse()
    {
      Inch i1 = new Inch(1.0);

      Assert.IsFalse(i1.Equals(5));
    }
  }
}