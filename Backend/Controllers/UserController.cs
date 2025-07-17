using Backend.DTOs;
using Backend.DTOs.User;
using Backend.Helpers;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users in the system.
        /// </summary>
        [HttpGet]
        [Authorize(Policy = Roles.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves user with provided unique identifier.
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize(Policy = Roles.Admin)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = await _userService.RegisterUserAsync(registerDto);
            if (userDto == null)
            {
                return Conflict("Email already exists");
            }

            return CreatedAtAction(nameof(GetById), new { id = userDto.UserId }, userDto);
        }

        /// <summary>
        /// Authenticates a user with the provided email and password.
        /// </summary>
        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = await _userService.LoginUserAsync(loginDto);

            if (userDto == null)
            {
                return Unauthorized("Email or Password incorrect");
            }

            return Ok(userDto);
        }

        /// <summary>
        /// Promotes/demotes a user with the provided role.
        /// </summary>
        [HttpPut("{id:guid}/role")]
        [Authorize(Policy = Roles.Admin)]
        public async Task<IActionResult> Promote([FromRoute] Guid id, [FromBody] PromoteUserDto promoteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = await _userService.PromoteUserAsync(id, promoteDto);

            if (userDto == null)
            {
                return NotFound("User not found or already has provided role.");
            }

            return Ok(userDto);
        }

        // Forgot password?, ChangePassword, DeleteUser
    }
}
