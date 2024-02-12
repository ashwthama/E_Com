using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        /// <summary>
        /// I am adding this controller for signal R integration 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllMessage()
        {

            return Ok();
        }

        public class ChatHub : Hub
        {
            public async Task SendMessage(string user, string message)
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
        }
    }
}
