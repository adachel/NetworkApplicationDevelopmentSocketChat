using Microsoft.AspNetCore.SignalR.Client;
using TChatClient;

HubConnection connection;
SignalRMessage signalRMessage = new SignalRMessage();

Console.WriteLine("Задать имя пользователя");
signalRMessage.FromUser.Name = Console.ReadLine();

connection = new HubConnectionBuilder().WithUrl("https://localhost:7000/chat").Build();

connection.On<SignalRMessage>("Receive", 
    signalRMessage => 
    Console.WriteLine($"Пользователь: {signalRMessage.FromUser.Name}\n" + $"Сообщение: {signalRMessage.Message}"));

await connection.StartAsync();

bool isExit = true;
while (isExit)
{
    Console.WriteLine("Введите сообщение");
    signalRMessage.Message = Console.ReadLine();

    if (signalRMessage.Message != "exit".ToLower())
    {
        await connection.SendAsync("Send", signalRMessage);
    }
    else isExit = false;
}
Console.ReadLine();
