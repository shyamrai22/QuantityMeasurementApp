using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityMeasurementApp.Model.Entity
{
  public class QuantityMeasurementEntity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Operation { get; set; }

    [MaxLength(100)]
    public string? Operand1 { get; set; }

    [MaxLength(100)]
    public string? Operand2 { get; set; }

    [MaxLength(100)]
    public string? Result { get; set; }

    [Required]
    [MaxLength(50)]
    public string MeasurementType { get; set; }

    [Required]
    public bool HasError { get; set; }

    [MaxLength(255)]
    public string? ErrorMessage { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  }
}