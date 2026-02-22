namespace QuantityMeasurementApp.Model
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

    private double ToFeet()
    {
      switch (unit)
      {
        case LengthUnit.FEET:
          return value;

        case LengthUnit.INCH:
          return value / 12.0;

        case LengthUnit.YARD:
          return value * 3.0; // 1 yard = 3 feet

        case LengthUnit.CENTIMETER:
          return (value * 0.393701) / 12.0; // cm -> inch -> feet

        default:
          throw new ArgumentException("Invalid choice");
      }
    }

    public Quantity Add(Quantity other)
    {
      if (other == null)
        throw new ArgumentException("Other quantity cannot be null");

      if (double.IsNaN(this.value) || double.IsInfinity(this.value) ||
          double.IsNaN(other.value) || double.IsInfinity(other.value))
        throw new ArgumentException("Invalid numeric value");

      double thisInFeet = this.ToFeet();
      double otherInFeet = other.ToFeet();

      double sumInFeet = thisInFeet + otherInFeet;

      double result = Convert(sumInFeet, LengthUnit.FEET, this.Unit);

      return new Quantity(result, this.Unit);
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(this, obj))
        return true;

      if (obj == null || GetType() != obj.GetType())
        return false;

      Quantity other = (Quantity)obj;

      return this.ToFeet().CompareTo(other.ToFeet()) == 0;
    }

    public override int GetHashCode()
    {
      return ToFeet().GetHashCode();
    }

    public static double Convert(double value, LengthUnit sourceUnit, LengthUnit targetUnit)
    {
      if (double.IsNaN(value) || double.IsInfinity(value))
        throw new ArgumentException("Invalid numeric value");

      if (!Enum.IsDefined(typeof(LengthUnit), sourceUnit) ||
          !Enum.IsDefined(typeof(LengthUnit), targetUnit))
        throw new ArgumentException("Invalid unit");

      Quantity temp = new Quantity(value, sourceUnit);
      double valueInFeet = temp.ToFeet();

      double valueInTargetUnit;
      switch (targetUnit)
      {
        case LengthUnit.INCH:
          valueInTargetUnit = valueInFeet * 12.0;
          break;

        case LengthUnit.FEET:
          valueInTargetUnit = valueInFeet;
          break;

        case LengthUnit.YARD:
          valueInTargetUnit = valueInFeet / 3.0;
          break;

        case LengthUnit.CENTIMETER:
          valueInTargetUnit = (valueInFeet * 12.0) / 0.393701;
          break;

        default:
          throw new ArgumentException("Invalid unit");
      }

      return valueInTargetUnit;
    }
  }
}