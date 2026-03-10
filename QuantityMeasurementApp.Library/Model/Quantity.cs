namespace QuantityMeasurementApp.Library.Model
{
  public class Quantity<T> where T : Enum
  {
    private readonly double value;
    private readonly T unit;

    public double Value => value;

    public T Unit => unit;

    public Quantity(double value, T unit)
    {
      if (!Enum.IsDefined(typeof(T), unit))
        throw new ArgumentException("Invalid unit");

      if (double.IsNaN(value) || double.IsInfinity(value))
        throw new ArgumentException("Invalid value");

      this.value = value;
      this.unit = unit;
    }

    public Quantity<T> ConvertTo(T targetUnit)
    {
      double baseValue = ConvertToBase(value, unit);
      double result = ConvertFromBase(baseValue, targetUnit);

      return new Quantity<T>(result, targetUnit);
    }

    public Quantity<T> Add(Quantity<T> other)
    {
      return Add(other, unit);
    }

    public Quantity<T> Add(Quantity<T> other, T targetUnit)
    {
      double base1 = ConvertToBase(value, unit);
      double base2 = ConvertToBase(other.value, other.unit);

      double sum = base1 + base2;

      double result = ConvertFromBase(sum, targetUnit);

      return new Quantity<T>(result, targetUnit);
    }

    private double ConvertToBase(double value, T unit)
    {
      if (unit is LengthUnit length)
        return LengthUnitExtensions.ToBaseUnit(length, value);

      if (unit is WeightUnit weight)
        return WeightUnitExtensions.ToBaseUnit(weight, value);

      if (unit is VolumeUnit volume)
        return VolumeUnitExtensions.ToBaseUnit(volume, value);

      throw new ArgumentException("Unsupported unit");
    }

    private double ConvertFromBase(double baseValue, T unit)
    {
      if (unit is LengthUnit length)
        return LengthUnitExtensions.FromBaseUnit(length, baseValue);

      if (unit is WeightUnit weight)
        return WeightUnitExtensions.FromBaseUnit(weight, baseValue);

      if (unit is VolumeUnit volume)
        return VolumeUnitExtensions.FromBaseUnit(volume, baseValue);

      throw new ArgumentException("Unsupported unit");
    }

    public Quantity<T> Subtract(Quantity<T> other)
    {
      return Subtract(other, Unit);
    }

    public Quantity<T> Subtract(Quantity<T> other, T targetUnit)
    {
      if (other == null)
        throw new ArgumentException("Quantity cannot be null");

      if (targetUnit == null)
        throw new ArgumentException("Target unit cannot be null");

      double baseValue1 = ConvertToBase(Value, Unit);
      double baseValue2 = ConvertToBase(other.Value, other.Unit);

      double resultBase = baseValue1 - baseValue2;

      double result = ConvertFromBase(resultBase, targetUnit);

      return new Quantity<T>(Math.Round(result, 2), targetUnit);
    }

    public double Divide(Quantity<T> other)
    {
      if (other == null)
        throw new ArgumentException("Quantity cannot be null");

      double baseValue1 = ConvertToBase(Value, Unit);
      double baseValue2 = ConvertToBase(other.Value, other.Unit);

      if (baseValue2 == 0)
        throw new ArithmeticException("Division by zero");

      return baseValue1 / baseValue2;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
        return false;

      Quantity<T> other = (Quantity<T>)obj;

      double base1 = ConvertToBase(value, unit);
      double base2 = ConvertToBase(other.value, other.unit);

      return Math.Abs(base1 - base2) < 0.0001;
    }

    public override int GetHashCode()
    {
      return ConvertToBase(value, unit).GetHashCode();
    }

  }
}