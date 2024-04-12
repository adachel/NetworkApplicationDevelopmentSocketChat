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

namespace WWWWWWWWWWW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {     
        HubConnection connection;  // подключение для взаимодействия с хабом.
                                   // Чтобы подключиться к хабу, применяется объект HubConnection.
        public MainWindow()
        {
            InitializeComponent();

            // создаем подключение к хабу
            connection = new HubConnectionBuilder()     // Для создания данного объекта применяется специальный класс-строитель
                                                        // HubConnectionBuilder.
                                                        // Через его метод WithUrl передается адрес, по которому доступен хаб.
                                                        // А метод Build() собственно создает объект подключения -
                                                        // объект HubConnection, через который мы можем взаимодействовать с хабом.
                .WithUrl("https://localhost:7000/chat")
                .Build();


            // регистрируем функцию Receive для получения данных
            connection.On<string, string>("Receive", (user, message) =>     // регистрируем метод, которая будет получать данные
                                                                            // от хаба с помощью метода connection.On().
                                                                            // Первый параметр представляет имя метода,
                                                                            // который будет получать данные от хаба,
                                                                            // второй параметр - собственно определение этого метода.
            {
                Dispatcher.Invoke(() =>                                     // Поэтому первый параметр метода connection.On представляет строка "Receive",
                                                                            // а второй параметр - метод или точнее лямбда-выражение получает отправленные
                                                                            // хабом данные в виде параметров. А поскольку хаб посылает две строки,
                                                                            // то метод connection.On() типизируется двумя типами string.
                {
                    var newMessage = $"{user}: {message}";                  // при получении данных от хаба с помощью метода Dispatcher.Invoke
                                                                            // мы можем обратиться к пользовательскому интерфейсу и добавить полученные данные
                                                                            // в список chatbox, который представляет элемент ListBox.
                    chatbox.Items.Insert(0, newMessage);
                });
            });
        }

        // обработчик загрузки окна
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // подключемся к хабу
                await connection.StartAsync();          // После получения объекта HubConnection для подключения к хабу надо вызвать метод StartAsync(). 
                chatbox.Items.Add("Вы вошли в чат");
                sendBtn.IsEnabled = true;
            }
            catch (Exception ex)
            {
                chatbox.Items.Add(ex.Message);
            }
        }
        // обработчик нажатия на кнопку
        private async void Button_Click(object sender, RoutedEventArgs e)   // Для отправки данных хабу у объекта HubConnection применяется метод InvokeAsync().
                                                                            // В клиенте на WPF отправка этому методу происходит в обработчике нажатия кнопки.
                                                                            // Первый параметр метода представляет имя метода хаба, к которому идет обращения.
                                                                            // Например, в данном случае это метод Send. Все последующие параметры передают данные
                                                                            // для параметров метода хаба. Так, метод Send в хабе ChatHub принимает два строковых параметра.
                                                                            // Соответственно в методе InvokeAsync мы можем передать для них данные.
                                                                            // Параметры передаются по позиции: второй аргумент метода InvokeAsync передает значение
                                                                            // для первого параметра метода Send, третий аргумент в InvokeAsync -
                                                                            // для второго параметра в Send и так далее.
        {
            try
            {
                // отправка сообщения
                await connection.InvokeAsync("Send", userTextBox.Text, messageTextBox.Text);
            }
            catch (Exception ex)
            {
                chatbox.Items.Add(ex.Message);
            }
        }
    }
}


/*
Отключение от хаба
При необходимости после подключения к хабу мы можем отключиться от него с помощью метода StopAsync() объекта HubConnection. 
Например, можно было бы определить для события Closing - события, которое возникает перед закрытием окна, следующий обработчик:
 
private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
{
    await connection.InvokeAsync("Send", "",$"Пользователь {userTextBox.Text} выходит из чата");
    await connection.StopAsync();   // отключение от хаба
}

HubConnection имеет ряд свойств, которые позволяют получить информацию о подключении или сконфигурировать клиент:

     - ConnectionId представляет индификатор текущего подключения

     - ServerTimeout представляет таймаут, в течение которого подключение считается активным. 
        Если в течение этого периода сервер не присылает никакого сообщения, то клиент считает, что подключение к серверу разорвано. 
        И в этом случае вызывается событие Closed().

     - KeepAliveInterval определяет интервал, в течение которого клиент посылает пинг-сообщения серверу. 
        Отправка любого сообщения от клиента сбрасывает таймер для отслеживания этого интервала до нуля.
        Если клиент не отправит никакого сообщения в течении интервала, который устанавливается свойством 
        ClientTimeoutInterval класса хаба на сервере, то сервер считает, что клиент отключился 

События HubConnection:

     - Closed возникает после закрытия подключения

     - Reconnected возникает после переподключения к хабу.

     - Reconnecting возникает перед переподключением к хабу.


Возможна ситуация, что соединение с хабом будет потеряно. Если мы хотим, чтобы подключение было восстановлено, 
то мы можем сделать это автоматически, используя у HubConnectionBuilder метод WithAutomaticReconnect():
connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7098/chat")
                .WithAutomaticReconnect()   // автопереподключение
                .Build();


При необходимости можно указать временные интервалы для переподключения с помощью массива TimeSpan:
connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7098/chat")
                .WithAutomaticReconnect(new[] { TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20) })
                .Build();


Перед переподключением возникает событие Reconnecting, а после переподключения - событие Reconnected. 
Соответственно, если небходимо отследить переподключение, то можно обработать данные события:
connection.Reconnecting += error =>
{
    // обработка события
 
    return Task.CompletedTask;
};
connection.Reconnected += connectionId =>
{
    // обработка события
 
    return Task.CompletedTask;
};

В обработчик события Reconnecting передается информация об ошибке в виде объекта Exception, 
а в обработчик события Reconnected передается новый идентификатор подключения.
Также можно вручную переподключиться с помощью обработки события Closed:
connection.Closed += async (error) =>
{
    await Task.Delay(1000); // черех секунду переподключаемся
    await connection.StartAsync();
};


Логгирование
У объекта HubConnectionBuilder есть метод ConfigureLogging(), в который передается делегат Action<Microsoft.Extensions.Logging.ILoggingBuilder>. 
Объект ILoggingBuilder позволяет настроить ряд параметров логгирования, в частности, с помощью метода SetMinimumLevel() устанавливается уровень логгирования:
connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7098/chat")
                .ConfigureLogging(logging => 
                {
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .Build();

Все возможные уровни логгирования:

LogLevel.None: логгирование отключено

LogLevel.Critical: логгирование сообщений об ошибках, которые относятся ко всему приложению в целом

LogLevel.Error: логгирование сообшений об ошибках, которые относятся к текущей операции

LogLevel.Warning: логгирование сообщений, которые не представляют ошибки

LogLevel.Information: логгирование информационных сообщений

LogLevel.Debug: логгирование диагностических сообщений, используемых при отладке

LogLevel.Trace: логгирование диагностических сообщений с детальной информацией


 */
