using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Library.Model;

namespace QuantityMeasurementApp.Tests
{
  [TestClass]
  public class GenericQuantityTest
  {
    const double EPSILON = 0.0001;
    /* =========================================
       IMeasurable INTERFACE TESTS
    ========================================= */

    [TestMethod]
    public void testIMeasurableInterface_LengthUnitImplementation()
    {
      LengthUnit unit = LengthUnit.FEET;

      double baseValue = unit.ToBaseUnit(1);

      Assert.AreEqual(1.0, baseValue, EPSILON);
    }

    [TestMethod]
    public void testIMeasurableInterface_WeightUnitImplementation()
    {
      WeightUnit unit = WeightUnit.KILOGRAM;

      double baseValue = unit.ToBaseUnit(1);

      Assert.AreEqual(1.0, baseValue, EPSILON);
    }

    [TestMethod]
    public void testIMeasurableInterface_ConsistentBehavior()
    {
      double lengthBase = LengthUnit.INCH.ToBaseUnit(12);
      double weightBase = WeightUnit.GRAM.ToBaseUnit(1000);

      Assert.AreEqual(1.0, lengthBase, EPSILON);
      Assert.AreEqual(1.0, weightBase, EPSILON);
    }

    /* =========================================
       GENERIC QUANTITY EQUALITY
    ========================================= */

    [TestMethod]
    public void testGenericQuantity_LengthOperations_Equality()
    {
      var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.INCH);

      Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void testGenericQuantity_WeightOperations_Equality()
    {
      var q1 = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM);
      var q2 = new Quantity<WeightUnit>(1000.0, WeightUnit.GRAM);

      Assert.IsTrue(q1.Equals(q2));
    }

    /* =========================================
       GENERIC QUANTITY CONVERSION
    ========================================= */

    [TestMethod]
    public void testGenericQuantity_LengthOperations_Conversion()
    {
      var q = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);

      var result = q.ConvertTo(LengthUnit.INCH);

      Assert.AreEqual(12.0, result.Value, EPSILON);
      Assert.AreEqual(LengthUnit.INCH, result.Unit);
    }

    [TestMethod]
    public void testGenericQuantity_WeightOperations_Conversion()
    {
      var q = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM);

      var result = q.ConvertTo(WeightUnit.GRAM);

      Assert.AreEqual(1000.0, result.Value, EPSILON);
      Assert.AreEqual(WeightUnit.GRAM, result.Unit);
    }

    /* =========================================
       GENERIC QUANTITY ADDITION
    ========================================= */

    [TestMethod]
    public void testGenericQuantity_LengthOperations_Addition()
    {
      var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.INCH);

      var result = q1.Add(q2, LengthUnit.FEET);

      Assert.AreEqual(2.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testGenericQuantity_WeightOperations_Addition()
    {
      var q1 = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM);
      var q2 = new Quantity<WeightUnit>(1000.0, WeightUnit.GRAM);

      var result = q1.Add(q2, WeightUnit.KILOGRAM);

      Assert.AreEqual(2.0, result.Value, EPSILON);
    }

    /* =========================================
       TYPE SAFETY TESTS
    ========================================= */

    [TestMethod]
    public void testCrossCategoryPrevention_LengthVsWeight()
    {
      var length = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
      var weight = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM);

      Assert.IsFalse(length.Equals(weight));
    }

    /* =========================================
       CONSTRUCTOR VALIDATION
    ========================================= */

    [TestMethod]
    public void testGenericQuantity_ConstructorValidation_InvalidUnit()
    {
      Assert.ThrowsException<ArgumentException>(() =>
      {
        var q = new Quantity<LengthUnit>(1.0, (LengthUnit)999);
      });
    }

    [TestMethod]
    public void testGenericQuantity_ConstructorValidation_InvalidValue()
    {
      Assert.ThrowsException<ArgumentException>(() =>
      {
        var q = new Quantity<LengthUnit>(double.NaN, LengthUnit.FEET);
      });
    }

    /* =========================================
       HASHCODE AND EQUALS CONTRACT
    ========================================= */

    [TestMethod]
    public void testHashCode_GenericQuantity_Consistency()
    {
      var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
      var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.INCH);

      Assert.AreEqual(q1.GetHashCode(), q2.GetHashCode());
    }

    [TestMethod]
    public void testEquals_GenericQuantity_ContractPreservation()
    {
      var a = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
      var b = new Quantity<LengthUnit>(12.0, LengthUnit.INCH);
      var c = new Quantity<LengthUnit>(0.333333, LengthUnit.YARD);

      Assert.IsTrue(a.Equals(b));
      Assert.IsTrue(b.Equals(c));
      Assert.IsTrue(a.Equals(c));
    }

    /* =========================================
       IMMUTABILITY TEST
    ========================================= */

    [TestMethod]
    public void testImmutability_GenericQuantity()
    {
      var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);

      var q2 = q1.ConvertTo(LengthUnit.INCH);

      Assert.AreNotSame(q1, q2);
    }
  }
}
