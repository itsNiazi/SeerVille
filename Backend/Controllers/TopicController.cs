using Backend.DTOs;
using Backend.Helpers;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        /// <summary>
        /// Retrieves all topics in the system.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var topics = await _topicService.GetAllAsync();
            return Ok(topics);
        }

        /// <summary>
        /// Retrieves topic with provided unique identifier.
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var topic = await _topicService.GetByIdAsync(id);
            if (topic == null)
            {
                return NotFound("Topic not found.");
            }

            return Ok(topic);
        }

        /// <summary>
        /// Creates a new topic in the system.
        /// </summary>
        [HttpPost]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> Create([FromBody] CreateTopicDto topicDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _topicService.CreateAsync(topicDto);
            return CreatedAtAction(nameof(GetById), new { id = created.TopicId }, created);
        }

        /// <summary>
        /// Deletes all topics in the system.
        /// </summary>
        [HttpDelete]
        [Authorize(Policy = Roles.Admin)]
        public async Task<IActionResult> DeleteAll()
        {
            await _topicService.DeleteAllAsync();
            return NoContent();
        }

        /// <summary>
        /// Deletes topic with the provided unique identifier.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var deletedTopic = await _topicService.DeleteByIdAsync(id);
            if (deletedTopic == null)
            {
                return NotFound("Topic not found.");
            }

            return NoContent();
        }

        /// <summary>
        /// Updates topic with provided unique identifier & modifications.
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTopicDto update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedTopic = await _topicService.UpdateAsync(id, update);

            if (updatedTopic == null)
            {
                return NotFound("Topic not found.");
            }

            return Ok(updatedTopic);
        }

    }
}
