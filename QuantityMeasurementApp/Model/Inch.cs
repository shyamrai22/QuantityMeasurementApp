namespace QuantityMeasurementApp.Model
{
  public class Inch
  {
    private readonly double value;

    public Inch(double value)
    {
      this.value = value;
    }

    public double Value => value;

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(this, obj))
        return true;

      if (obj == null || GetType() != obj.GetType())
        return false;

      Inch other = (Inch)obj;

      return this.value.CompareTo(other.value) == 0;
    }

    public override int GetHashCode()
    {
      return value.GetHashCode();
    }
  }
}