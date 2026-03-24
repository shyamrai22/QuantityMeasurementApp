using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementApp.Model.DTO
{
  public class CompareRequest
  {
    [Required]
    public QuantityDTO First { get; set; }

    [Required]
    public QuantityDTO Second { get; set; }
  }
}