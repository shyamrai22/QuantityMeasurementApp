using System;

namespace QuantityMeasurementApp.Library.Model
{
  public class Quantity
  {
    private readonly double value;
    private readonly LengthUnit unit;

    public Quantity(double value, LengthUnit unit)
    {
      this.value = value;
      this.unit = unit;
    }

    public double Value => value;
    public LengthUnit Unit => unit;

    private double AddInFeet(Quantity other)
    {
      return unit.ToBaseUnit(value) + other.unit.ToBaseUnit(other.value);
    }

    // UC6
    public Quantity Add(Quantity other)
    {
      if (other == null)
        throw new ArgumentException("Other quantity cannot be null");

      if (double.IsNaN(this.value) || double.IsInfinity(this.value) ||
          double.IsNaN(other.value) || double.IsInfinity(other.value))
        throw new ArgumentException("Invalid numeric value");

      double sumInFeet = AddInFeet(other);

      double result = this.unit.FromBaseUnit(sumInFeet);

      return new Quantity(result, this.unit);
    }

    // UC7
    public Quantity Add(Quantity other, LengthUnit targetUnit)
    {
      if (other == null)
        throw new ArgumentException("Other quantity cannot be null");

      if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
        throw new ArgumentException("Invalid target unit");

      if (double.IsNaN(this.value) || double.IsInfinity(this.value) ||
          double.IsNaN(other.value) || double.IsInfinity(other.value))
        throw new ArgumentException("Invalid numeric value");

      double sumInFeet = AddInFeet(other);

      double result = targetUnit.FromBaseUnit(sumInFeet);

      return new Quantity(result, targetUnit);
    }

    // UC1
    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(this, obj))
        return true;

      if (obj == null || GetType() != obj.GetType())
        return false;

      Quantity other = (Quantity)obj;

      return unit.ToBaseUnit(value)
          .CompareTo(other.unit.ToBaseUnit(other.value)) == 0;
    }

    public override int GetHashCode()
    {
      return unit.ToBaseUnit(value).GetHashCode();
    }

    // UC5
    public static double Convert(double value, LengthUnit sourceUnit, LengthUnit targetUnit)
    {
      if (double.IsNaN(value) || double.IsInfinity(value))
        throw new ArgumentException("Invalid numeric value");

      if (!Enum.IsDefined(typeof(LengthUnit), sourceUnit) ||
          !Enum.IsDefined(typeof(LengthUnit), targetUnit))
        throw new ArgumentException("Invalid unit");

      double baseValue = sourceUnit.ToBaseUnit(value);

      return targetUnit.FromBaseUnit(baseValue);
    }
  }
}