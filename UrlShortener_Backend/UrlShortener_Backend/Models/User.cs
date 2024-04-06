using System.ComponentModel.DataAnnotations;

namespace UrlShortener_Backend.Models;

public class User
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Role { get; set; }

    [MaxLength(50)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Login { get; set; }

    [Required]
    [MaxLength(60)]
    public string? PasswordHash { get; set; }

    [Required]
    [MaxLength(60)]
    public string? Salt { get; set; }

    [Timestamp]
    public byte[]? Version { get; set; }
}
