using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  [Required]
  [EmailAddress]
  [MaxLength(100)]
  public string Email { get; set; }

  [Required]
  public string PasswordHash { get; set; }

  [Required]
  [MaxLength(20)]
  public string Role { get; set; } = "User";
}