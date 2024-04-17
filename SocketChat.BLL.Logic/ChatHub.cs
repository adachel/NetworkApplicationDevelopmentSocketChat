using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocketChat.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketChat.BLL.Logic
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("It`s okay, you are connected");
        }

        public async Task Send(SignalRMessage message)
        {
            await Clients.Others.SendAsync("Receive", message);
        }

        public async Task SendToUser(SignalRMessage message)
        {
            var client = Clients.Client(message.ConnectionId);
            await client.SendAsync($"message: {message.Message}; fromUser: {message.FromUser}");
        }
    }
}
