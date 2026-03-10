using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Library.Model;

namespace QuantityMeasurementApp.Tests
{
  [TestClass]
  public class QuantityWeightTest
  {
    const double EPSILON = 0.0001;

    [TestMethod]
    public void testEquality_KilogramToKilogram_SameValue()
    {
      var w1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

      Assert.IsTrue(w1.Equals(w2));
    }

    [TestMethod]
    public void testEquality_KilogramToKilogram_DifferentValue()
    {
      var w1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(2.0, WeightUnit.KILOGRAM);

      Assert.IsFalse(w1.Equals(w2));
    }

    [TestMethod]
    public void testEquality_KilogramToGram_EquivalentValue()
    {
      var w1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(1000.0, WeightUnit.GRAM);

      Assert.IsTrue(w1.Equals(w2));
    }

    [TestMethod]
    public void testEquality_GramToKilogram_EquivalentValue()
    {
      var w1 = new QuantityWeight(1000.0, WeightUnit.GRAM);
      var w2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

      Assert.IsTrue(w1.Equals(w2));
    }

    [TestMethod]
    public void testEquality_WeightVsLength_Incompatible()
    {
      var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
      var length = new Quantity(1.0, LengthUnit.FEET);

      Assert.IsFalse(weight.Equals(length));
    }

    [TestMethod]
    public void testEquality_NullComparison()
    {
      var w1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

      Assert.IsFalse(w1.Equals(null));
    }

    [TestMethod]
    public void testEquality_SameReference()
    {
      var w1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

      Assert.IsTrue(w1.Equals(w1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void testEquality_NullUnit()
    {
      new QuantityWeight(1.0, (WeightUnit)999);
    }

    [TestMethod]
    public void testEquality_TransitiveProperty()
    {
      var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
      var b = new QuantityWeight(1000.0, WeightUnit.GRAM);
      var c = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

      Assert.IsTrue(a.Equals(b));
      Assert.IsTrue(b.Equals(c));
      Assert.IsTrue(a.Equals(c));
    }

    [TestMethod]
    public void testEquality_ZeroValue()
    {
      var w1 = new QuantityWeight(0.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(0.0, WeightUnit.GRAM);

      Assert.IsTrue(w1.Equals(w2));
    }

    [TestMethod]
    public void testEquality_NegativeWeight()
    {
      var w1 = new QuantityWeight(-1.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(-1000.0, WeightUnit.GRAM);

      Assert.IsTrue(w1.Equals(w2));
    }

    [TestMethod]
    public void testEquality_LargeWeightValue()
    {
      var w1 = new QuantityWeight(1000000.0, WeightUnit.GRAM);
      var w2 = new QuantityWeight(1000.0, WeightUnit.KILOGRAM);

      Assert.IsTrue(w1.Equals(w2));
    }

    [TestMethod]
    public void testEquality_SmallWeightValue()
    {
      var w1 = new QuantityWeight(0.001, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(1.0, WeightUnit.GRAM);

      Assert.IsTrue(w1.Equals(w2));
    }

    [TestMethod]
    public void testConversion_PoundToKilogram()
    {
      var w = new QuantityWeight(2.20462, WeightUnit.POUND);

      var result = w.ConvertTo(WeightUnit.KILOGRAM);

      Assert.AreEqual(1.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_KilogramToPound()
    {
      var w = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

      var result = w.ConvertTo(WeightUnit.POUND);

      Assert.AreEqual(2.20462, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_SameUnit()
    {
      var w = new QuantityWeight(5.0, WeightUnit.KILOGRAM);

      var result = w.ConvertTo(WeightUnit.KILOGRAM);

      Assert.AreEqual(5.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_ZeroValue()
    {
      var w = new QuantityWeight(0.0, WeightUnit.KILOGRAM);

      var result = w.ConvertTo(WeightUnit.GRAM);

      Assert.AreEqual(0.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_NegativeValue()
    {
      var w = new QuantityWeight(-1.0, WeightUnit.KILOGRAM);

      var result = w.ConvertTo(WeightUnit.GRAM);

      Assert.AreEqual(-1000.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_RoundTrip()
    {
      var w = new QuantityWeight(1.5, WeightUnit.KILOGRAM);

      var result = w.ConvertTo(WeightUnit.GRAM)
                    .ConvertTo(WeightUnit.KILOGRAM);

      Assert.AreEqual(1.5, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_SameUnit_KilogramPlusKilogram()
    {
      var w1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(2.0, WeightUnit.KILOGRAM);

      var result = w1.Add(w2);

      Assert.AreEqual(3.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_CrossUnit_KilogramPlusGram()
    {
      var w1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(1000.0, WeightUnit.GRAM);

      var result = w1.Add(w2);

      Assert.AreEqual(2.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_CrossUnit_PoundPlusKilogram()
    {
      var w1 = new QuantityWeight(2.20462, WeightUnit.POUND);
      var w2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

      var result = w1.Add(w2);

      Assert.AreEqual(4.40924, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_ExplicitTargetUnit_Kilogram()
    {
      var w1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(1000.0, WeightUnit.GRAM);

      var result = w1.Add(w2, WeightUnit.GRAM);

      Assert.AreEqual(2000.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_Commutativity()
    {
      var w1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(1000.0, WeightUnit.GRAM);

      var result1 = w1.Add(w2);
      var result2 = w2.Add(w1);

      Assert.AreEqual(
          w1.Unit.ToBaseUnit(result1.Value),
          w2.Unit.ToBaseUnit(result2.Value),
          EPSILON
      );
    }

    [TestMethod]
    public void testAddition_WithZero()
    {
      var w1 = new QuantityWeight(5.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(0.0, WeightUnit.GRAM);

      var result = w1.Add(w2);

      Assert.AreEqual(5.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_NegativeValues()
    {
      var w1 = new QuantityWeight(5.0, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(-2000.0, WeightUnit.GRAM);

      var result = w1.Add(w2);

      Assert.AreEqual(3.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_LargeValues()
    {
      var w1 = new QuantityWeight(1e6, WeightUnit.KILOGRAM);
      var w2 = new QuantityWeight(1e6, WeightUnit.KILOGRAM);

      var result = w1.Add(w2);

      Assert.AreEqual(2e6, result.Value, EPSILON);
    }
  }
}