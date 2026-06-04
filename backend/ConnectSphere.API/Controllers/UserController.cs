using ConnectSphere.API.DTOs;
using ConnectSphere.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConnectSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var profile = await _userService.GetProfileAsync(userId);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var profile = await _userService.GetProfileAsync(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _userService.UpdateProfileAsync(userId, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("follow/{id}")]
        public async Task<IActionResult> Follow(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _userService.FollowUserAsync(userId, id);
            if (!result) return BadRequest(new { message = "Already following or invalid" });
            return Ok(new { message = "Followed successfully" });
        }

        [HttpDelete("unfollow/{id}")]
        public async Task<IActionResult> Unfollow(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _userService.UnfollowUserAsync(userId, id);
            if (!result) return BadRequest(new { message = "Not following this user" });
            return Ok(new { message = "Unfollowed successfully" });
        }
    }
}