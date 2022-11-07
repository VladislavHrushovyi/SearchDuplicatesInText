using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Models;

public class UserModel
{
    [Required]
    [MaxLength(25)]
    [MinLength(5)]
    public string Nickname { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}