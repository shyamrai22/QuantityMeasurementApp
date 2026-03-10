namespace QuantityMeasurementApp.Library.Model
{
  public static class TemperatureUnitExtensions
  {
    public static double ToBaseUnit(TemperatureUnit unit, double value)
    {
      switch (unit)
      {
        case TemperatureUnit.CELSIUS:
          return value;

        case TemperatureUnit.FAHRENHEIT:
          return (value - 32) * 5.0 / 9.0;

        case TemperatureUnit.KELVIN:
          return value - 273.15;

        default:
          throw new ArgumentException("Unsupported temperature unit");
      }
    }

    public static double FromBaseUnit(TemperatureUnit unit, double baseValue)
    {
      switch (unit)
      {
        case TemperatureUnit.CELSIUS:
          return baseValue;

        case TemperatureUnit.FAHRENHEIT:
          return (baseValue * 9.0 / 5.0) + 32;

        case TemperatureUnit.KELVIN:
          return baseValue + 273.15;

        default:
          throw new ArgumentException("Unsupported temperature unit");
      }
    }
  }
}