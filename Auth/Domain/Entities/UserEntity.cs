using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Auth.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    [MinLength(5, ErrorMessage = "Nickname has been have more than 5 characters")]
    [MaxLength(25, ErrorMessage = "Nickname has been maximum 25 characters")]
    public string Nickname { get; set; }

    [EmailAddress]
    public string Login { get; set; }
    [MinLength(6)]
    public string Password { get; set; }
}