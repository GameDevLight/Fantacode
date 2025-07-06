using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginDashboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        [Authorize]
        [HttpGet("data")]
        public IActionResult GetDashboardData()
        {
            var data = new[]
            {
                new { label = "Open", value = 10 },
                new { label = "Closed", value = 5 },
                new { label = "In Progress", value = 7 }
            };
            return Ok(data);
        }
    }
}
