using System.ComponentModel.DataAnnotations;

namespace MontyTask.Data.Resources;
public class UserCredentialsResource
{
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(32)]
    public string Password { get; set; } = string.Empty;
}