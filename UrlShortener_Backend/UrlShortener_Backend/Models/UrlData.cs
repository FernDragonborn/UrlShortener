using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener_Backend.Models;

public class UrlData
{
    [Key]
    public string? ShortUrl { get; set; }

    [Required]
    public string? LongUrl { get; set; }

    [Required]
    [ForeignKey("UserId")]
    internal virtual User User { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public int Clicks { get; set; } = 0;

    [Timestamp]
    public byte[] Version { get; set; }
}
