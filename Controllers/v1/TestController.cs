using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("This is TestController v1");
        }
    }
}
