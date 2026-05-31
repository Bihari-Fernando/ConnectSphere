using System.ComponentModel.DataAnnotations;

namespace ConnectSphere.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        public int SenderId { get; set; }
        public User Sender { get; set; } = null!;
        public int ReceiverId { get; set; }
        public User Receiver { get; set; } = null!;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}