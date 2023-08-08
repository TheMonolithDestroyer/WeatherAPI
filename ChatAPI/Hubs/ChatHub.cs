using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(string user, string message)
        {
            await Console.Out.WriteLineAsync("dasdasdasd");
            await Clients.All.ReceiveMessage(user, message);
        }

        public async Task SendMessageToCaller(string user, string message)
        {
            await Console.Out.WriteLineAsync("dasdasdasd1212112");
            await Clients.Caller.ReceiveMessage(user, message);
        }

        public async Task SendMessageToGroup(string user, string message)
        {
            await Console.Out.WriteLineAsync("2313123123");
            await Clients.Group("SignalR Users").ReceiveMessage(user, message);
        }
    }
}
