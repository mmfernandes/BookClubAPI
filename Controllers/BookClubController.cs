using Microsoft.AspNetCore.Mvc;
using BookClubAPI.Models;
using BookClubAPI.Services;

namespace BookClubAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookClubController : ControllerBase
    {
        private readonly BookClubService _service = new();

        [HttpPost("event")]
        public IActionResult ProcessEvent([FromBody] EventModel eventModel)
        {
            var result = _service.ProcessEvent(eventModel);
            return Ok(result);
        }

        [HttpGet("balanced")]
        public IActionResult GetBalancedStatus()
        {
            var result = _service.GetBalancedStatus();
            return Ok(result);
        }
    }
}
