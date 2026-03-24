using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementApp.Model.DTO
{
  public class QuantityDTO
  {
    [Required]
    [Range(0.0001, double.MaxValue, ErrorMessage = "Value must be greater than 0")]
    public double Value { get; set; }

    [Required]
    [MaxLength(20)]
    public string Unit { get; set; }

    [Required]
    [MaxLength(20)]
    public string MeasurementType { get; set; }

    public QuantityDTO() { }

    public QuantityDTO(double value, string unit, string type)
    {
      Value = value;
      Unit = unit;
      MeasurementType = type;
    }
  }
}