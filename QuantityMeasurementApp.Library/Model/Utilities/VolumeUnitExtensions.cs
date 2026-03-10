namespace QuantityMeasurementApp.Library.Model
{
  public static class VolumeUnitExtensions
  {
    public static double ToBaseUnit(this VolumeUnit unit, double value)
    {
      switch (unit)
      {
        case VolumeUnit.LITRE:
          return value;

        case VolumeUnit.MILLILITRE:
          return value * 0.001;

        case VolumeUnit.GALLON:
          return value * 3.78541;

        default:
          throw new ArgumentException("Invalid volume unit");
      }
    }

    public static double FromBaseUnit(this VolumeUnit unit, double baseValue)
    {
      switch (unit)
      {
        case VolumeUnit.LITRE:
          return baseValue;

        case VolumeUnit.MILLILITRE:
          return baseValue / 0.001;

        case VolumeUnit.GALLON:
          return baseValue / 3.78541;

        default:
          throw new ArgumentException("Invalid volume unit");
      }
    }
  }
}