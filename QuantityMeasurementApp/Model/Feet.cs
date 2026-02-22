namespace QuantityMeasurementApp.Model
{
  public class Feet
  {
    private readonly double value;

    public Feet(double value)
    {
      this.value = value;
    }

    public double Value => value;

    public override bool Equals(object obj)
    {
      // Step 1: Same reference (Reflexive)
      if (ReferenceEquals(this, obj))
        return true;

      // Step 2: Null or different type
      if (obj == null || GetType() != obj.GetType())
        return false;

      // Step 3: Safe casting
      Feet other = (Feet)obj;

      // Step 4: Compare values properly
      return this.value.CompareTo(other.value) == 0;
    }

    public override int GetHashCode()
    {
      return value.GetHashCode();
    }
  }
}