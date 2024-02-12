using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllMessage()
        {
            return Ok();
        }
    }
}
