using System.Security.Claims;
using Backend.DTOs;
using Backend.DTOs.Prediction;
using Backend.Helpers;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly IPredictionService _predictionService;

        public PredictionController(IPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        /// <summary>
        /// Retrieves all predictions in the system.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var predictions = await _predictionService.GetAllAsync();
            return Ok(predictions);
        }

        [HttpGet("votes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllWithVotes()   //Should be with query object?
        {
            var predictions = await _predictionService.GetAllWithVotesAsync();
            return Ok(predictions);
        }

        /// <summary>
        /// Retrieves prediction with provided unique identifier.
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var prediction = await _predictionService.GetByIdAsync(id);
            if (prediction == null)
            {
                return NotFound("Prediction not found.");
            }
            return Ok(prediction);
        }

        // SHOULD BE HANDLED AS QUERY PARAM??
        [HttpGet("topic/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByTopicId([FromRoute] Guid id)
        {
            var predictions = await _predictionService.GetByTopicIdAsync(id);
            if (predictions == null)
            {
                return NotFound("Predictions not found.");
            }
            return Ok(predictions);
        }

        /// <summary>
        /// Creates prediction in the system with provided prediction details.
        /// </summary>
        [HttpPost]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> Create([FromBody] CreatePredictionDto createDto)
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

            var createdPrediction = await _predictionService.CreateAsync(userId, createDto);
            if (createdPrediction == null)
            {
                return NotFound("Topic with provided ID not found.");
            }

            return CreatedAtAction(nameof(GetById), new { id = createdPrediction.PredictionId }, createdPrediction);
        }

        /// <summary>
        /// Updates prediction with provided unique identifier.
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdatePredictionDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPrediction = await _predictionService.UpdateByIdAsync(id, updateDto);
            if (updatedPrediction == null)
            {
                return NotFound("Prediction with provided ID not found.");
            }
            return Ok(updatedPrediction);
        }

        /// <summary>
        /// Patches the prediction status.
        /// </summary>
        [HttpPatch("{id:guid}")]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> PatchById([FromRoute] Guid id, [FromBody] PatchPredictionDto patchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resolverId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(resolverId))
            {
                return Unauthorized("User identifier not found.");
            }

            var patchedPrediction = await _predictionService.PatchByIdAsync(id, resolverId, patchDto);
            if (patchedPrediction == null)
            {
                return NotFound("Prediction with provided ID not found.");
            }

            return Ok(patchedPrediction);
        }

        /// <summary>
        /// Deletes all predictions in the system.
        /// </summary>
        [HttpDelete]
        [Authorize(Policy = Roles.Admin)]
        public async Task<IActionResult> DeleteAll()
        {
            await _predictionService.DeleteAllAsync();
            return NoContent();
        }

        /// <summary>
        /// Deletes prediction with provided unique identifier.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {

            var deletedPrediction = await _predictionService.DeleteByIdAsync(id);
            if (deletedPrediction == null)
            {
                return NotFound("Prediction with provided ID not found.");
            }
            return NoContent();
        }

    }
}
