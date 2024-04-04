using System.ComponentModel.DataAnnotations;

namespace UrlShortener_Backend.Models
{
    public class UrlData
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? LongUrl { get; set; }

        [Required]
        public string? ShortUrl { get; set; }

        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
