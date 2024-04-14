using Microsoft.AspNetCore.SignalR.Client;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TSocketClient
{
    public partial class MainWindow : Window
    {
        HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7000/chat")
                .Build();

            connection.On<SignalRMessage>("Receive", (message) =>
            {
                Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{message.FromUser}: {message.Message}";
                    chatbox.Items.Insert(0, newMessage);
                });
            });
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.StartAsync();
                chatbox.Items.Add("Вы вошли в чат");
                sendBtn.IsEnabled = true;
            }
            catch (Exception ex)
            {
                chatbox.Items.Add(ex.Message);
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SignalRMessage signalRMessage = new SignalRMessage();
                signalRMessage.FromUser = userTextBox.Text;
                signalRMessage.Message = messageTextBox.Text;

                await connection.InvokeAsync("Send", signalRMessage);
            }
            catch (Exception ex)
            {
                chatbox.Items.Add(ex.Message);
            }
        }





















    }
}