using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Library.Model;

namespace QuantityMeasurementApp.Tests
{
  [TestClass]
  public class QuantityArithmeticTests
  {
    const double EPSILON = 0.0001;

    /* =========================
       SUBTRACTION TESTS
    ========================= */

    [TestMethod]
    public void testSubtraction_SameUnit_FeetMinusFeet()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);

      var result = q1.Subtract(q2);

      Assert.AreEqual(5.0, result.Value, EPSILON);
      Assert.AreEqual(LengthUnit.FEET, result.Unit);
    }

    [TestMethod]
    public void testSubtraction_SameUnit_LitreMinusLitre()
    {
      var q1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.LITRE);
      var q2 = new Quantity<VolumeUnit>(3.0, VolumeUnit.LITRE);

      var result = q1.Subtract(q2);

      Assert.AreEqual(7.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_CrossUnit_FeetMinusInches()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(6.0, LengthUnit.INCH);

      var result = q1.Subtract(q2);

      Assert.AreEqual(9.5, result.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_CrossUnit_InchesMinusFeet()
    {
      var q1 = new Quantity<LengthUnit>(120.0, LengthUnit.INCH);
      var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);

      var result = q1.Subtract(q2);

      Assert.AreEqual(60.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_ExplicitTargetUnit_Feet()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(6.0, LengthUnit.INCH);

      var result = q1.Subtract(q2, LengthUnit.FEET);

      Assert.AreEqual(9.5, result.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_ExplicitTargetUnit_Inches()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(6.0, LengthUnit.INCH);

      var result = q1.Subtract(q2, LengthUnit.INCH);

      Assert.AreEqual(114.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_ExplicitTargetUnit_Millilitre()
    {
      var q1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.LITRE);
      var q2 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE);

      var result = q1.Subtract(q2, VolumeUnit.MILLILITRE);

      Assert.AreEqual(3000.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_ResultingInNegative()
    {
      var q1 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);

      var result = q1.Subtract(q2);

      Assert.AreEqual(-5.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_ResultingInZero()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(120.0, LengthUnit.INCH);

      var result = q1.Subtract(q2);

      Assert.AreEqual(0.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_WithZeroOperand()
    {
      var q1 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(0.0, LengthUnit.INCH);

      var result = q1.Subtract(q2);

      Assert.AreEqual(5.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_NonCommutative()
    {
      var a = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var b = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);

      var result1 = a.Subtract(b);
      var result2 = b.Subtract(a);

      Assert.AreEqual(5.0, result1.Value, EPSILON);
      Assert.AreEqual(-5.0, result2.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_ChainedOperations()
    {
      var q = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);

      var result = q.Subtract(new Quantity<LengthUnit>(2.0, LengthUnit.FEET))
                    .Subtract(new Quantity<LengthUnit>(1.0, LengthUnit.FEET));

      Assert.AreEqual(7.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testSubtraction_NullOperand()
    {
      var q = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);

      Assert.ThrowsException<ArgumentException>(() =>
          q.Subtract(null));
    }

    /* =========================
       DIVISION TESTS
    ========================= */

    [TestMethod]
    public void testDivision_SameUnit_FeetDividedByFeet()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);

      var result = q1.Divide(q2);

      Assert.AreEqual(5.0, result, EPSILON);
    }

    [TestMethod]
    public void testDivision_SameUnit_LitreDividedByLitre()
    {
      var q1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.LITRE);
      var q2 = new Quantity<VolumeUnit>(5.0, VolumeUnit.LITRE);

      var result = q1.Divide(q2);

      Assert.AreEqual(2.0, result, EPSILON);
    }

    [TestMethod]
    public void testDivision_CrossUnit_FeetDividedByInches()
    {
      var q1 = new Quantity<LengthUnit>(24.0, LengthUnit.INCH);
      var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);

      var result = q1.Divide(q2);

      Assert.AreEqual(1.0, result, EPSILON);
    }

    [TestMethod]
    public void testDivision_CrossUnit_KilogramDividedByGram()
    {
      var q1 = new Quantity<WeightUnit>(2.0, WeightUnit.KILOGRAM);
      var q2 = new Quantity<WeightUnit>(2000.0, WeightUnit.GRAM);

      var result = q1.Divide(q2);

      Assert.AreEqual(1.0, result, EPSILON);
    }

    [TestMethod]
    public void testDivision_RatioGreaterThanOne()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);

      var result = q1.Divide(q2);

      Assert.AreEqual(5.0, result, EPSILON);
    }

    [TestMethod]
    public void testDivision_RatioLessThanOne()
    {
      var q1 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);

      var result = q1.Divide(q2);

      Assert.AreEqual(0.5, result, EPSILON);
    }

    [TestMethod]
    public void testDivision_RatioEqualToOne()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);

      var result = q1.Divide(q2);

      Assert.AreEqual(1.0, result, EPSILON);
    }

    [TestMethod]
    public void testDivision_NonCommutative()
    {
      var a = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var b = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);

      var r1 = a.Divide(b);
      var r2 = b.Divide(a);

      Assert.AreEqual(2.0, r1, EPSILON);
      Assert.AreEqual(0.5, r2, EPSILON);
    }

    [TestMethod]
    public void testDivision_ByZero()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(0.0, LengthUnit.FEET);

      Assert.ThrowsException<ArithmeticException>(() =>
          q1.Divide(q2));
    }

    [TestMethod]
    public void testDivision_NullOperand()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);

      Assert.ThrowsException<ArgumentException>(() =>
          q1.Divide(null));
    }

    [TestMethod]
    public void testSubtraction_Immutability()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);

      var result = q1.Subtract(q2);

      Assert.AreNotSame(q1, result);
    }

    [TestMethod]
    public void testDivision_Immutability()
    {
      var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);

      var result = q1.Divide(q2);

      Assert.AreEqual(5.0, result, EPSILON);
    }
  }
}