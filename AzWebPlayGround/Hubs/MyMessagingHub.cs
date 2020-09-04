using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using AzWebPlayGround.Services;
using Microsoft.AspNetCore.Http;

namespace AzWebPlayGround.Hubs
{
    public interface IMyClient
    {
        Task SendTextMessage(string message, string connectionId);
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
}