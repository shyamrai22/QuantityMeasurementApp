using System;

namespace QuantityMeasurementApp.Library.Model
{
  public class QuantityWeight
  {
    private readonly double value;
    private readonly WeightUnit unit;

    public QuantityWeight(double value, WeightUnit unit)
    {
      if (!Enum.IsDefined(typeof(WeightUnit), unit))
        throw new ArgumentException("Invalid weight unit");

      if (double.IsNaN(value) || double.IsInfinity(value))
        throw new ArgumentException("Invalid numeric value");

      this.value = value;
      this.unit = unit;
    }

    public double Value => value;
    public WeightUnit Unit => unit;

    private double AddInKilograms(QuantityWeight other)
    {
      return unit.ToBaseUnit(value) + other.unit.ToBaseUnit(other.value);
    }

    public QuantityWeight Add(QuantityWeight other)
    {
      if (other == null)
        throw new ArgumentException("Other weight cannot be null");

      double sumKg = AddInKilograms(other);
      double result = unit.FromBaseUnit(sumKg);

      return new QuantityWeight(result, unit);
    }

    public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
    {
      if (other == null)
        throw new ArgumentException("Other weight cannot be null");

      double sumKg = AddInKilograms(other);
      double result = targetUnit.FromBaseUnit(sumKg);

      return new QuantityWeight(result, targetUnit);
    }

    public QuantityWeight ConvertTo(WeightUnit targetUnit)
    {
      double baseValue = unit.ToBaseUnit(value);
      double converted = targetUnit.FromBaseUnit(baseValue);

      return new QuantityWeight(converted, targetUnit);
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(this, obj))
        return true;

      if (obj == null || GetType() != obj.GetType())
        return false;

      QuantityWeight other = (QuantityWeight)obj;

      return unit.ToBaseUnit(value)
          .CompareTo(other.unit.ToBaseUnit(other.value)) == 0;
    }

    public override int GetHashCode()
    {
      return unit.ToBaseUnit(value).GetHashCode();
    }

    public override string ToString()
    {
      return $"Quantity({value}, {unit})";
    }
  }
}