using System.ComponentModel.DataAnnotations;

namespace ConnectSphere.API.Models
{
    public class Post
    {
        public int Id { get; set; }
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        public string? ImageUrl { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // AI sentiment score
        public float? SentimentScore { get; set; }
        public string? SentimentLabel { get; set; }

        // Navigation properties
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}