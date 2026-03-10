using System;

namespace QuantityMeasurementApp.Library.Model
{
  public static class WeightUnitExtensions
  {
    // Convert weight → base unit (kilogram)
    public static double ToBaseUnit(this WeightUnit unit, double value)
    {
      return unit switch
      {
        WeightUnit.KILOGRAM => value,
        WeightUnit.GRAM => value * 0.001,
        WeightUnit.POUND => value * 0.453592,
        _ => throw new ArgumentException("Invalid weight unit")
      };
    }

    // Convert from kilogram → target unit
    public static double FromBaseUnit(this WeightUnit unit, double baseValue)
    {
      return unit switch
      {
        WeightUnit.KILOGRAM => baseValue,
        WeightUnit.GRAM => baseValue * 1000,
        WeightUnit.POUND => baseValue / 0.453592,
        _ => throw new ArgumentException("Invalid weight unit")
      };
    }
  }
}