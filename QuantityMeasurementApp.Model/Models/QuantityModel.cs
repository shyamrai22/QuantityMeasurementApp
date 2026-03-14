namespace QuantityMeasurementApp.Model.Models
{
  public class QuantityModel<T> where T : Enum
  {
    public double Value { get; }
    public T Unit { get; }

    public QuantityModel(double value, T unit)
    {
      Value = value;
      Unit = unit;
    }
  }
}