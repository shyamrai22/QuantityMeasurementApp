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

        default:
          throw new ArgumentException("Invalid choice");
      }
    }

    public override bool Equals(object obj)
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
  }
}