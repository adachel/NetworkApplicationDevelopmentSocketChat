using Microsoft.AspNetCore.SignalR.Client;
using TChatClient;

HubConnection connection;
User user = new User();
SignalRMessage signalRMessage = new SignalRMessage();

Console.WriteLine("Задать имя пользователя");
user.Name = Console.ReadLine();

connection = new HubConnectionBuilder().WithUrl("https://localhost:7000/chat").Build();

connection.On<SignalRMessage>("Receive", 
    signalRMessage => 
    Console.WriteLine($"Пользователь: {user.Name}\n" + $"Сообщение: {signalRMessage.MessageContent}"));

await connection.StartAsync();

bool isExit = true;
while (isExit)
{
    Console.WriteLine("Введите сообщение");
    signalRMessage.MessageContent = Console.ReadLine();

    if (signalRMessage.MessageContent != "exit".ToLower())
    {
        await connection.SendAsync("Send", signalRMessage);
    }
    else isExit = false;
}
Console.ReadLine();
