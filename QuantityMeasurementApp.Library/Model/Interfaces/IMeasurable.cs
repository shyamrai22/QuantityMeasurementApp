namespace QuantityMeasurementApp.Library.Model
{
  public interface IMeasurable
  {
    double ToBaseUnit(double value);
    double FromBaseUnit(double baseValue);
  }
}