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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var predictions = await _predictionService.GetAllAsync();
            return Ok(predictions);
        }

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

        // [HttpGet("{topic}")]
        // [AllowAnonymous]
        // public async Task<IActionResult> GetByTopicId([FromRoute] string topic)
        // {
        //     var prediction = await _predictionService.GetByTopicIdAsync(topic);
        //     if (prediction == null)
        //     {
        //         return BadRequest("Invalid topic name");
        //     }
        //     return Ok(prediction);
        // }

        [HttpPost]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> Create([FromBody] CreatePredictionDto createDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdPrediction = await _predictionService.CreateAsync(userId, createDto);
            if (createdPrediction == null)
            {
                return BadRequest("topic id wrong!"); //?
            }

            return Ok(createdPrediction); //CreatedAt!
        }

        // [HttpPut("{id:guid}")]
        // [Authorize(Policy = Roles.Moderator)]
        // public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdatePredictionDto updateDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     var updatedPrediction = await _predictionService.UpdateByIdAsync(id, updateDto);
        //     if (updatedPrediction == null)
        //     {
        //         return BadRequest("Invalid inputs");
        //     }
        //     return Ok(updatedPrediction);
        // }

        // [HttpPatch("{id:guid}")]
        // [Authorize(Policy = Roles.Moderator)]
        // public async Task<IActionResult> PatchById([FromRoute] Guid id, [FromBody] PatchPredictionDto patchDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        // }

        // [HttpDelete]
        // [Authorize(Policy = Roles.Admin)]
        // public async Task<IActionResult> DeleteAll()
        // {
        //     //Not async?
        //     //Delete All
        //     return Ok();
        // }


        // [HttpDelete("{id:guid}")]
        // [Authorize(Policy = Roles.Moderator)]
        // public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        // {
        //     //Not async?
        //     //Delete by ID
        //     return Ok();
        // }


    }
}
