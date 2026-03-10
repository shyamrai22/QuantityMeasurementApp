namespace QuantityMeasurementApp.Library.Model
{
  public interface IMeasurable
  {
    double ToBaseUnit(double value);
    double FromBaseUnit(double baseValue);

    bool SupportsArithmetic()
    {
      return true;
    }

    void ValidateOperationSupport(string operation)
    {
      // default does nothing
    }
  }
}