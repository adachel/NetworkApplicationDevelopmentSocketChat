using Microsoft.AspNetCore.SignalR;

namespace TTTTTTTTTTTTTTTTTTTTTTT
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)  // получает некоторое отправленное сообщение в виде параметра message
                                                // и затем с помощью вызова await Clients.All.SendAsync("Send", message)
                                                // ретранслирует это сообщение всем подключенным клиентам.
        {
            await this.Clients.All.SendAsync("Receive", message);   // Первый параметр метода SendAsync() указывает на метод,
                                                                    // который будет получать ответ от сервера.
                                                                    // Второй параметр представляет набор значений,
                                                                    // которые посылаются в ответе клиенту.
                                                                    // То есть метод Receive на клиенте получит значение параметра message. 
        }
    }
}
