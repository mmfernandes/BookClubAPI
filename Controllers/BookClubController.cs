using Microsoft.AspNetCore.Mvc;
using BookClubAPI.Models;
using BookClubAPI.Services;

namespace BookClubAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookClubController : ControllerBase
    {
        private readonly BookClubService _service;

        public BookClubController(BookClubService service)
        {
            _service = service;
        }

        [HttpPost("event")]
        public IActionResult ProcessEvent([FromBody] EventModel eventModel)
        {
            var result = _service.ProcessEvent(eventModel);
            return Ok(result);
        }

        [HttpGet("balanced")]
        public IActionResult GetBalancedUsers()
        {
            var result = _service.GetBalancedStatus();
            return Ok(result);
        }
    }
}
