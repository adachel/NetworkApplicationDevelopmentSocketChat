using Microsoft.AspNetCore.SignalR;
using SocketChat.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketChat.BLL.Logic
{
    public class ChatHub : Hub // точка, к ней будут подключаться различные клиенты
    {
        // принимаем подключение, нужно уведомить клиента, что он подключился
        public override async Task OnConnectedAsync() // когда клиент подключится к сокету, отработает этот метод
        {
            await Clients.Caller.SendAsync("It`s okay, you are connected"); // к клиенту отправится сообщение
        }


        public async Task Send(SignalRMessage message /* message */)
        {
            // await Clients.All.SendAsync(message); // опссылаем сообщения всем клиентам, кот подключены

            //await Clients.All.SendAsync($"message: {message.Message}; fromUser: {message.FromUser}"); // опссылаем объект всем клиентам, кот подключены

            await Clients.All.SendAsync($"message: {message.Message}; fromUser: {Context.ConnectionId}"); // опссылаем объект всем клиентам, кот подключены
        }


        // если хотим делать отправку какому-то клиенту.
        // В методе OnConnectedAsync() каждому клиенту присваивается ConnectionId.
        // Идентификация: можем сделать, так, что, клиент когда подключался, присылал свои данные.
        // По этом уникальным данным сохраняем в список его ConnectionId и эти данные.
        // Чтобы какому-то конкретному клиенту отправить сообщение, в списке ищем клиента,
        // получаем ConnectionId и этом клиенту отправляем сообщение по ConnectionId. 
        public async Task SendToUser(SignalRMessage message)
        {
            var client = Clients.Client(message.ConnectionId);
            await client.SendAsync($"message: {message.Message}; fromUser: {message.FromUser}");
        }
    }
}
