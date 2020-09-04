using AzWebPlayGround.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace AzWebPlayGround.Hubs
{
    public interface IMyClient
    {
        Task SendTextMessage(string message, string connectionId);
        Task SendChatMessage(MyUserMessageModel message);
    }

    public class MyMessagingHub : Hub<IMyClient>
    {
        private readonly IUserService _userService;

        public MyMessagingHub(IUserService userService)
        {
            _userService = userService;
        }

        public async Task EchoTextMessage(string message, string connectionId)
        {
            await Clients.All.SendTextMessage(message, connectionId);
        }

        public async Task EchoChatMessage(string message)
        {
            var currentUser = Context.User;
            var user = currentUser?.Identity?.Name;
            var myUserMessageModel = new MyUserMessageModel
            {
                User = user,
                ConnectionId = Context.ConnectionId,
                Message = message
            };
            await Clients.All.SendChatMessage(myUserMessageModel);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var currentUser = Context.User; // as MyUserPrincipal;
            if (currentUser!=null)
            {
                var user = currentUser?.Identity?.Name;
                await EchoTextMessage($"User: {user} Joined", this.Context.ConnectionId);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }

    public class MyUserMessageModel
    {
        public string? User { get; set; }
        public string ConnectionId { get; set; }
        public string Message { get; set; }
    }
}