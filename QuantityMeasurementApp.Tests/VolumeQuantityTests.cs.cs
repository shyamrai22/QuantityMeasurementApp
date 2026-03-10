using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Library.Model;

namespace QuantityMeasurementApp.Tests
{
  [TestClass]
  public class VolumeQuantityTests
  {
    const double EPSILON = 0.0001;

    /* ======================================
       EQUALITY TESTS
    ====================================== */

    [TestMethod]
    public void testEquality_LitreToLitre_SameValue()
    {
      var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);

      Assert.IsTrue(v1.Equals(v2));
    }

    [TestMethod]
    public void testEquality_LitreToLitre_DifferentValue()
    {
      var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE);

      Assert.IsFalse(v1.Equals(v2));
    }

    [TestMethod]
    public void testEquality_LitreToMillilitre_EquivalentValue()
    {
      var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);

      Assert.IsTrue(v1.Equals(v2));
    }

    [TestMethod]
    public void testEquality_MillilitreToLitre_EquivalentValue()
    {
      var v1 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);
      var v2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);

      Assert.IsTrue(v1.Equals(v2));
    }

    [TestMethod]
    public void testEquality_LitreToGallon_EquivalentValue()
    {
      var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(0.264172, VolumeUnit.GALLON);

      Assert.IsTrue(v1.Equals(v2));
    }

    [TestMethod]
    public void testEquality_GallonToLitre_EquivalentValue()
    {
      var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.GALLON);
      var v2 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE);

      Assert.IsTrue(v1.Equals(v2));
    }

    [TestMethod]
    public void testEquality_VolumeVsLength_Incompatible()
    {
      var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var length = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);

      Assert.IsFalse(volume.Equals(length));
    }

    [TestMethod]
    public void testEquality_VolumeVsWeight_Incompatible()
    {
      var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var weight = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM);

      Assert.IsFalse(volume.Equals(weight));
    }

    [TestMethod]
    public void testEquality_NullComparison()
    {
      var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);

      Assert.IsFalse(volume.Equals(null));
    }

    [TestMethod]
    public void testEquality_SameReference()
    {
      var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);

      Assert.IsTrue(volume.Equals(volume));
    }

    [TestMethod]
    public void testEquality_TransitiveProperty()
    {
      var a = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var b = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);
      var c = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);

      Assert.IsTrue(a.Equals(b));
      Assert.IsTrue(b.Equals(c));
      Assert.IsTrue(a.Equals(c));
    }

    [TestMethod]
    public void testEquality_ZeroValue()
    {
      var v1 = new Quantity<VolumeUnit>(0.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(0.0, VolumeUnit.MILLILITRE);

      Assert.IsTrue(v1.Equals(v2));
    }

    [TestMethod]
    public void testEquality_NegativeVolume()
    {
      var v1 = new Quantity<VolumeUnit>(-1.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(-1000.0, VolumeUnit.MILLILITRE);

      Assert.IsTrue(v1.Equals(v2));
    }

    [TestMethod]
    public void testEquality_LargeVolumeValue()
    {
      var v1 = new Quantity<VolumeUnit>(1000000.0, VolumeUnit.MILLILITRE);
      var v2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.LITRE);

      Assert.IsTrue(v1.Equals(v2));
    }

    [TestMethod]
    public void testEquality_SmallVolumeValue()
    {
      var v1 = new Quantity<VolumeUnit>(0.001, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.MILLILITRE);

      Assert.IsTrue(v1.Equals(v2));
    }

    /* ======================================
       CONVERSION TESTS
    ====================================== */

    [TestMethod]
    public void testConversion_LitreToMillilitre()
    {
      var v = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);

      var result = v.ConvertTo(VolumeUnit.MILLILITRE);

      Assert.AreEqual(1000.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_MillilitreToLitre()
    {
      var v = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);

      var result = v.ConvertTo(VolumeUnit.LITRE);

      Assert.AreEqual(1.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_GallonToLitre()
    {
      var v = new Quantity<VolumeUnit>(1.0, VolumeUnit.GALLON);

      var result = v.ConvertTo(VolumeUnit.LITRE);

      Assert.AreEqual(3.78541, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_LitreToGallon()
    {
      var v = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE);

      var result = v.ConvertTo(VolumeUnit.GALLON);

      Assert.AreEqual(1.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_MillilitreToGallon()
    {
      var v = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);

      var result = v.ConvertTo(VolumeUnit.GALLON);

      Assert.AreEqual(0.264172, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_SameUnit()
    {
      var v = new Quantity<VolumeUnit>(5.0, VolumeUnit.LITRE);

      var result = v.ConvertTo(VolumeUnit.LITRE);

      Assert.AreEqual(5.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_ZeroValue()
    {
      var v = new Quantity<VolumeUnit>(0.0, VolumeUnit.LITRE);

      var result = v.ConvertTo(VolumeUnit.MILLILITRE);

      Assert.AreEqual(0.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_NegativeValue()
    {
      var v = new Quantity<VolumeUnit>(-1.0, VolumeUnit.LITRE);

      var result = v.ConvertTo(VolumeUnit.MILLILITRE);

      Assert.AreEqual(-1000.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testConversion_RoundTrip()
    {
      var v = new Quantity<VolumeUnit>(1.5, VolumeUnit.LITRE);

      var result = v.ConvertTo(VolumeUnit.MILLILITRE)
                    .ConvertTo(VolumeUnit.LITRE);

      Assert.AreEqual(1.5, result.Value, EPSILON);
    }

    /* ======================================
       ADDITION TESTS
    ====================================== */

    [TestMethod]
    public void testAddition_SameUnit_LitrePlusLitre()
    {
      var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE);

      var result = v1.Add(v2);

      Assert.AreEqual(3.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_SameUnit_MillilitrePlusMillilitre()
    {
      var v1 = new Quantity<VolumeUnit>(500.0, VolumeUnit.MILLILITRE);
      var v2 = new Quantity<VolumeUnit>(500.0, VolumeUnit.MILLILITRE);

      var result = v1.Add(v2);

      Assert.AreEqual(1000.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_CrossUnit_LitrePlusMillilitre()
    {
      var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);

      var result = v1.Add(v2);

      Assert.AreEqual(2.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_CrossUnit_MillilitrePlusLitre()
    {
      var v1 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);
      var v2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);

      var result = v1.Add(v2);

      Assert.AreEqual(2000.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_CrossUnit_GallonPlusLitre()
    {
      var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.GALLON);
      var v2 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE);

      var result = v1.Add(v2);

      Assert.AreEqual(2.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_ExplicitTargetUnit_Litre()
    {
      var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);

      var result = v1.Add(v2, VolumeUnit.LITRE);

      Assert.AreEqual(2.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_ExplicitTargetUnit_Millilitre()
    {
      var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);

      var result = v1.Add(v2, VolumeUnit.MILLILITRE);

      Assert.AreEqual(2000.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_WithZero()
    {
      var v1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(0.0, VolumeUnit.MILLILITRE);

      var result = v1.Add(v2);

      Assert.AreEqual(5.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_NegativeValues()
    {
      var v1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(-2000.0, VolumeUnit.MILLILITRE);

      var result = v1.Add(v2);

      Assert.AreEqual(3.0, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_LargeValues()
    {
      var v1 = new Quantity<VolumeUnit>(1e6, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(1e6, VolumeUnit.LITRE);

      var result = v1.Add(v2);

      Assert.AreEqual(2e6, result.Value, EPSILON);
    }

    [TestMethod]
    public void testAddition_SmallValues()
    {
      var v1 = new Quantity<VolumeUnit>(0.001, VolumeUnit.LITRE);
      var v2 = new Quantity<VolumeUnit>(0.002, VolumeUnit.LITRE);

      var result = v1.Add(v2);

      Assert.AreEqual(0.003, result.Value, EPSILON);
    }
  }
}