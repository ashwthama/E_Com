using E_Commerce.Domain.Models.Chat;
using E_Commerce.Domain.Models.DataStorage;
using E_Commerce.Domain.Models.TimerFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hub;
        private readonly TimerManager _timer;

        public ChatController(IHubContext<ChatHub> hub, TimerManager timer)
        {
            _hub = hub;
            _timer = timer;
        }


        [HttpGet]
        public IActionResult Get()
        {
            if (!_timer.IsTimerStarted)
                _timer.PrepareTimer(() => _hub.Clients.All.SendAsync("TransferChartData", DataManager.GetData()));
            return Ok(new { Message = "Request Completed" });
        }

    }
}
