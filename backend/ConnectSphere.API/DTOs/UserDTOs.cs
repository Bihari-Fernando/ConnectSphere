namespace ConnectSphere.API.DTOs
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Location { get; set; }
        public string? Headline { get; set; }
        public string? Skills { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
    }

    public class UpdateProfileDTO
    {
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public string? Headline { get; set; }
        public string? Skills { get; set; }
    }
}