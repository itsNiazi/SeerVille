using System.Security.Claims;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionVoteController : ControllerBase
    {
        private readonly IPredictionVoteService _predictionVoteService;

        public PredictionVoteController(IPredictionVoteService predictionVoteService)
        {
            _predictionVoteService = predictionVoteService;
        }

        // GET VOTES BY USER ID
        [HttpGet]
        public async Task<IActionResult> GetAllByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User identifier not found.");
            }

            var votes = await _predictionVoteService.GetAllByIdAsync(userId);
            if (votes == null)
            {
                return NotFound("Votes with provided ID not found.");
            }

            return Ok(votes);
        }

        // POST VOTE
        [HttpPost]
        public async Task<IActionResult> Vote([FromBody] CreatePredictionVoteDto voteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User identifier not found.");
            }

            var registeredVote = await _predictionVoteService.VoteAsync(userId, voteDto);
            if (registeredVote == null)
            {
                return BadRequest("Unable to vote.");
            }

            // return CreatedAtAction(nameof()) need to implement
            return Ok(registeredVote);

        }
    }
}
