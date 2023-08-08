using ChatAPI.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductOfferController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;
        public ProductOfferController(IHubContext<ChatHub, IChatClient> chatHub)
        {
            _chatHub = chatHub;
        }
        [HttpPost]
        public string Get()
        {

            _chatHub.Clients.All.ReceiveMessage("test", "1213");

            return "ok";
        }
    }
}
