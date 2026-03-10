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

    private static double ConvertToBase(double value, T unit)
    {
      if (unit is LengthUnit l)
        return l.ToBaseUnit(value);

      if (unit is WeightUnit w)
        return w.ToBaseUnit(value);

      throw new ArgumentException("Unsupported unit");
    }

    private static double ConvertFromBase(double value, T unit)
    {
      if (unit is LengthUnit l)
        return l.FromBaseUnit(value);

      if (unit is WeightUnit w)
        return w.FromBaseUnit(value);

      throw new ArgumentException("Unsupported unit");
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