using System;

namespace QuantityMeasurementApp.Library.Model
{
  public static class LengthUnitExtensions
  {
    // Convert value from this unit → base unit (feet)
    public static double ToBaseUnit(this LengthUnit unit, double value)
    {
      return unit switch
      {
        LengthUnit.FEET => value,
        LengthUnit.INCH => value / 12.0,
        LengthUnit.YARD => value * 3.0,
        LengthUnit.CENTIMETER => value / 30.48,
        _ => throw new ArgumentException("Invalid unit")
      };
    }

    // Convert value from base unit (feet) → this unit
    public static double FromBaseUnit(this LengthUnit unit, double baseValue)
    {
      return unit switch
      {
        LengthUnit.FEET => baseValue,
        LengthUnit.INCH => baseValue * 12.0,
        LengthUnit.YARD => baseValue / 3.0,
        LengthUnit.CENTIMETER => baseValue * 30.48,
        _ => throw new ArgumentException("Invalid unit")
      };
    }
  }
}