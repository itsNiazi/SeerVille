using Backend.DTOs;
using Backend.Helpers;
using Backend.Interfaces;
using Backend.Mappers;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var topics = await _topicService.GetAllAsync();
            return Ok(topics);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var topic = await _topicService.GetByIdAsync(id);
            return topic == null ? NotFound("Topic not found.") : Ok(topic); // convention to send modelstate?
        }

        [HttpPost]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> Create([FromBody] CreateTopicDto topicDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _topicService.CreateAsync(topicDto);
            return CreatedAtAction(nameof(GetById), new { id = created.TopicId }, created.ToTopicDto()); //Clarify
        }

        [HttpDelete]
        [Authorize(Policy = Roles.Admin)]
        public async Task<IActionResult> DeleteAll()
        {
            await _topicService.DeleteAllAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var deletedTopic = await _topicService.DeleteByIdAsync(id);
            if (deletedTopic == null) return NotFound("Topic not found.");

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = Roles.Moderator)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTopicDto update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedTopic = await _topicService.UpdateAsync(id, update);

            return updatedTopic == null ? NotFound("Topic not found") : Ok(updatedTopic); //updated status code?
        }

    }
}
