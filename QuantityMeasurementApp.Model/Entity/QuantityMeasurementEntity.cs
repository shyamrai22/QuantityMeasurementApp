namespace QuantityMeasurementApp.Model.Entity
{
  public class QuantityMeasurementEntity
  {
    public string Operation { get; }
    public object Operand1 { get; }
    public object Operand2 { get; }
    public object Result { get; }
    public bool HasError { get; }
    public string ErrorMessage { get; }

    public QuantityMeasurementEntity(
        string operation,
        object operand1,
        object operand2,
        object result)
    {
      Operation = operation;
      Operand1 = operand1;
      Operand2 = operand2;
      Result = result;
    }

    public QuantityMeasurementEntity(string error)
    {
      HasError = true;
      ErrorMessage = error;
    }
  }
}