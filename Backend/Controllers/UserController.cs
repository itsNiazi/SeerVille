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

        [HttpGet]
        [Authorize(Policy = Roles.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

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

            return Ok(userDto); //CreatedAt!
        }

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

            return Ok(userDto); //authenticated status code?
        }

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
                return BadRequest("");
            }

            return Ok(userDto); //Updated status code?
        }

        // ChangePassword, DeleteUser

    }
}
