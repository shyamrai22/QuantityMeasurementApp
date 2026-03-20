namespace QuantityMeasurementApp.Model.Entity
{
  public class QuantityMeasurementEntity
  {
    public string Operation { get; }
    public string? Operand1 { get; }
    public string? Operand2 { get; }
    public string? Result { get; }
    public string MeasurementType { get; }
    public bool HasError { get; }
    public string? ErrorMessage { get; }
    public DateTime CreatedAt { get; }

    public QuantityMeasurementEntity(
        string operation,
        string? operand1,
        string? operand2,
        string? result,
        string measurementType,
        bool hasError = false,
        string? errorMessage = null)
    {
      Operation = operation;
      Operand1 = operand1;
      Operand2 = operand2;
      Result = result;
      MeasurementType = measurementType;
      HasError = hasError;
      ErrorMessage = errorMessage;
      CreatedAt = DateTime.UtcNow;
    }
  }
}