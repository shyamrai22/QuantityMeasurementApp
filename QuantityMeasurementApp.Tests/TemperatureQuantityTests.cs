using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Library.Model;

namespace QuantityMeasurementApp.Tests
{
  [TestClass]
  public class TemperatureQuantityTests
  {
    const double EPSILON = 0.0001;

    /* =============================
       TEMPERATURE EQUALITY TESTS
    ============================= */

    [TestMethod]
    public void testTemperatureEquality_CelsiusToCelsius_SameValue()
    {
      var t1 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS);
      var t2 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS);

      Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void testTemperatureEquality_FahrenheitToFahrenheit_SameValue()
    {
      var t1 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT);
      var t2 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT);

      Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void testTemperatureEquality_CelsiusToFahrenheit_0Celsius32Fahrenheit()
    {
      var t1 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS);
      var t2 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT);

      Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void testTemperatureEquality_CelsiusToFahrenheit_100Celsius212Fahrenheit()
    {
      var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
      var t2 = new Quantity<TemperatureUnit>(212.0, TemperatureUnit.FAHRENHEIT);

      Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void testTemperatureEquality_CelsiusToFahrenheit_Negative40Equal()
    {
      var t1 = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.CELSIUS);
      var t2 = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.FAHRENHEIT);

      Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void testTemperatureEquality_SymmetricProperty()
    {
      var t1 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS);
      var t2 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT);

      Assert.IsTrue(t1.Equals(t2));
      Assert.IsTrue(t2.Equals(t1));
    }

    [TestMethod]
    public void testTemperatureEquality_ReflexiveProperty()
    {
      var t = new Quantity<TemperatureUnit>(25.0, TemperatureUnit.CELSIUS);

      Assert.IsTrue(t.Equals(t));
    }

    /* =============================
       TEMPERATURE CONVERSION TESTS
    ============================= */

    [TestMethod]
    public void testTemperatureConversion_SameUnit()
    {
      var temp = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);

      var result = temp.ConvertTo(TemperatureUnit.CELSIUS);

      Assert.AreEqual(50.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testTemperatureConversion_NegativeValues()
    {
      var temp = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.CELSIUS);

      var result = temp.ConvertTo(TemperatureUnit.FAHRENHEIT);

      Assert.AreEqual(-40.0, result.Value, EPSILON);
    }

    /* =============================
       UNSUPPORTED ARITHMETIC TESTS
    ============================= */

    [TestMethod]
    public void testTemperatureUnsupportedOperation_Add()
    {
      var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
      var t2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);

      Assert.ThrowsException<NotSupportedException>(() =>
      {
        t1.Add(t2);
      });
    }

    [TestMethod]
    public void testTemperatureUnsupportedOperation_Subtract()
    {
      var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
      var t2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);

      Assert.ThrowsException<NotSupportedException>(() =>
      {
        t1.Subtract(t2);
      });
    }

    [TestMethod]
    public void testTemperatureUnsupportedOperation_Divide()
    {
      var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
      var t2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);

      Assert.ThrowsException<NotSupportedException>(() =>
      {
        t1.Divide(t2);
      });
    }

    /* =============================
       CROSS CATEGORY TESTS
    ============================= */

    [TestMethod]
    public void testTemperatureVsLengthIncompatibility()
    {
      var temp = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
      var length = new Quantity<LengthUnit>(100.0, LengthUnit.FEET);

      Assert.IsFalse(temp.Equals(length));
    }

    [TestMethod]
    public void testTemperatureVsWeightIncompatibility()
    {
      var temp = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);
      var weight = new Quantity<WeightUnit>(50.0, WeightUnit.KILOGRAM);

      Assert.IsFalse(temp.Equals(weight));
    }

    [TestMethod]
    public void testTemperatureVsVolumeIncompatibility()
    {
      var temp = new Quantity<TemperatureUnit>(25.0, TemperatureUnit.CELSIUS);
      var volume = new Quantity<VolumeUnit>(25.0, VolumeUnit.LITRE);

      Assert.IsFalse(temp.Equals(volume));
    }

    /* =============================
       NULL SAFETY
    ============================= */

    [TestMethod]
    public void testTemperatureNullOperandValidation_InComparison()
    {
      var temp = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);

      Assert.IsFalse(temp.Equals(null));
    }
  }
}