using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Utility
{
  public static class MeasurementHelper
  {
    public static bool AreFeetEqual(double value1, double value2)
    {
      Feet f1 = new Feet(value1);
      Feet f2 = new Feet(value2);

      return f1.Equals(f2);
    }

    public static bool AreInchesEqual(double value1, double value2)
    {
      Inch i1 = new Inch(value1);
      Inch i2 = new Inch(value2);

      return i1.Equals(i2);
    }
  }
}