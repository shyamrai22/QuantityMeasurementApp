namespace QuantityMeasurementApp.Library.Model
{
  public class Quantity<T> where T : Enum
  {
    private readonly double value;
    private readonly T unit;

    public double Value => value;
    public T Unit => unit;

    private enum ArithmeticOperation
    {
      ADD,
      SUBTRACT,
      DIVIDE
    }

    public Quantity(double value, T unit)
    {
      if (!Enum.IsDefined(typeof(T), unit))
        throw new ArgumentException("Invalid unit");

      if (double.IsNaN(value) || double.IsInfinity(value))
        throw new ArgumentException("Invalid value");

      this.value = value;
      this.unit = unit;
    }

    /* =========================
       CENTRALIZED VALIDATION
    ========================= */

    private void ValidateArithmeticOperands(Quantity<T> other, T targetUnit, bool targetUnitRequired)
    {
      if (other == null)
        throw new ArgumentException("Operand cannot be null");

      if (!double.IsFinite(this.Value) || !double.IsFinite(other.Value))
        throw new ArgumentException("Values must be finite");

      if (this.Unit.GetType() != other.Unit.GetType())
        throw new ArgumentException("Measurement categories must match");

      if (targetUnitRequired && targetUnit == null)
        throw new ArgumentException("Target unit cannot be null");
    }

    /* =========================
       CENTRALIZED ARITHMETIC
    ========================= */

    private double PerformBaseArithmetic(Quantity<T> other, ArithmeticOperation operation)
    {
      double base1 = ConvertToBase(value, unit);
      double base2 = ConvertToBase(other.value, other.unit);

      switch (operation)
      {
        case ArithmeticOperation.ADD:
          return base1 + base2;

        case ArithmeticOperation.SUBTRACT:
          return base1 - base2;

        case ArithmeticOperation.DIVIDE:
          if (base2 == 0)
            throw new ArithmeticException("Division by zero");
          return base1 / base2;

        default:
          throw new ArgumentException("Unsupported arithmetic operation");
      }
    }

    /* =========================
       CONVERSION
    ========================= */

    public Quantity<T> ConvertTo(T targetUnit)
    {
      double baseValue = ConvertToBase(value, unit);
      double result = ConvertFromBase(baseValue, targetUnit);

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

    /* =========================
       ADD
    ========================= */

    public Quantity<T> Add(Quantity<T> other)
    {
      return Add(other, unit);
    }

    public Quantity<T> Add(Quantity<T> other, T targetUnit)
    {
      ValidateArithmeticOperands(other, targetUnit, true);

      double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);

      double result = ConvertFromBase(baseResult, targetUnit);

      return new Quantity<T>(Math.Round(result, 2), targetUnit);
    }

    /* =========================
       SUBTRACT
    ========================= */

    public Quantity<T> Subtract(Quantity<T> other)
    {
      return Subtract(other, unit);
    }

    public Quantity<T> Subtract(Quantity<T> other, T targetUnit)
    {
      ValidateArithmeticOperands(other, targetUnit, true);

      double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);

      double result = ConvertFromBase(baseResult, targetUnit);

      return new Quantity<T>(Math.Round(result, 2), targetUnit);
    }

    /* =========================
       DIVIDE
    ========================= */

    public double Divide(Quantity<T> other)
    {
      ValidateArithmeticOperands(other, default, false);

      return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
    }

    /* =========================
       EQUALITY
    ========================= */

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