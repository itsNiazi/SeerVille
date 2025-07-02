using Backend.DTOs;
using Backend.DTOs.User;
using Backend.Interfaces;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = await _userService.LoginUserAsync(loginDto);

            if (userDto == null)
            {
                return BadRequest("Email or Password incorrect");
            }


            return Ok(userDto);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = await _userService.RegisterUserAsync(registerDto);
            if (userDto == null)
            {
                return BadRequest("Email already exists"); // since usernames are unique as well, we will have collision in the future!!
            }


            return Ok(userDto);
        }

        // ChangePassword, DeleteUser, ToModerator, ToAdmin
    }
}
