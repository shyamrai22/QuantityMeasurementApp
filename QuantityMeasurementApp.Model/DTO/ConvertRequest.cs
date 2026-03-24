using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementApp.Model.DTO
{
  public class ConvertRequest
  {
    [Required]
    public QuantityDTO Source { get; set; }

    [Required]
    [MaxLength(20)]
    public string TargetUnit { get; set; }
  }
}