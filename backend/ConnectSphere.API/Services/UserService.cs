using ConnectSphere.API.Data;
using ConnectSphere.API.DTOs;
using ConnectSphere.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.API.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfileDTO?> GetProfileAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return null;

            return MapToProfileDTO(user);
        }

        public async Task<List<UserProfileDTO>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .Where(u => u.IsActive)
                .ToListAsync();

            return users.Select(MapToProfileDTO).ToList();
        }

        public async Task<UserProfileDTO?> UpdateProfileAsync(int userId, UpdateProfileDTO dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            user.Bio = dto.Bio ?? user.Bio;
            user.Location = dto.Location ?? user.Location;
            user.Headline = dto.Headline ?? user.Headline;
            user.Skills = dto.Skills ?? user.Skills;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetProfileAsync(userId);
        }

        public async Task<bool> FollowUserAsync(int followerId, int followingId)
        {
            if (followerId == followingId) return false;

            var exists = await _context.Follows
                .AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);

            if (exists) return false;

            _context.Follows.Add(new Follow
            {
                FollowerId = followerId,
                FollowingId = followingId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnfollowUserAsync(int followerId, int followingId)
        {
            var follow = await _context.Follows
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);

            if (follow == null) return false;

            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
            return true;
        }

        private UserProfileDTO MapToProfileDTO(User user)
        {
            return new UserProfileDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Bio = user.Bio,
                ProfilePicture = user.ProfilePicture,
                Location = user.Location,
                Headline = user.Headline,
                Skills = user.Skills,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                FollowersCount = user.Followers.Count,
                FollowingCount = user.Following.Count
            };
        }
    }
}