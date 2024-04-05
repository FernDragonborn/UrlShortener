using UrlShortener_Backend.Models;

namespace UrlShortener_Backend.DTOs
{
    public class UrlDto
    {
        public UrlDto(UrlData url)
        {
            ShortUrl = url.ShortUrl;
            LongUrl = url.LongUrl;
            UserId = url.User.UserId.ToString();
            CreatedAt = url.CreatedAt;
            Clicks = url.Clicks;
        }

        public string? ShortUrl { get; set; }

        public string? LongUrl { get; set; }

        internal string? UserId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? Clicks { get; set; }
    }
}
