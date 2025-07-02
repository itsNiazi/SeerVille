using Backend.DTOs;
using Backend.Interfaces;
using Backend.Mappers;
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
        /// [GET]: api/topic
        /// </summary>
        /// <returns>All topics</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var topics = await _topicService.GetAllAsync();
            return Ok(topics);
        }

        /// <summary>
        /// [GET]: api/topic/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Topic based on provided id</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var topic = await _topicService.GetByIdAsync(id);
            return topic == null ? NotFound(ModelState) : Ok(topic); // convention to send modelstate?
        }

        /// <summary>
        /// [POST]: api/topic
        /// </summary>
        /// <param name="topic"></param>
        /// <returns>Created topic details</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTopicDto topicDto)
        {
            // Still need valiadation for white spaces and special characters
            if (!ModelState.IsValid)
            {
                return BadRequest("Oh no!");
            }

            var created = await _topicService.CreateAsync(topicDto);
            return CreatedAtAction(nameof(GetById), new { id = created.TopicId }, created.ToTopicDto()); //Clarify
        }

        /// <summary>
        /// [DELETE]: api/topic
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await _topicService.DeleteAllAsync();
            return NoContent();
        }

        /// <summary>
        /// [DELETE]: api/topic/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var deletedTopic = await _topicService.DeleteByIdAsync(id);
            if (deletedTopic == null) return NotFound();

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] PatchTopicDto patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Oh, no!");
            }
            var patchedTopic = await _topicService.PatchAsync(id, patch);

            return patchedTopic == null ? NotFound() : Ok(patchedTopic);
        }
    }
}
