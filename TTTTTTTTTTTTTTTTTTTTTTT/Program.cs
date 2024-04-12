using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;
using System.Net.NetworkInformation;
using System.Net.WebSockets;

namespace TTTTTTTTTTTTTTTTTTTTTTT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            /////////////////////////////////////////////////////////////////////////////////////////////////
            // builder.Services.AddSignalR(); // добавляем в приложение сервисы SignalR

            // Класс HubOptions определяет ряд свойств:
            // ClientTimeoutInterval. Определяет время, в течение которого клиент должен отправить серверу сообщение.
            // Если в течение данного времени никаких сообщенй от клиента на сервер не пришло, то сервер закрывает соединение.
            // По умолчанию равно 30 секунд.

            // HandshakeTimeout.После подключения к серверу клиент должен отправить серверу
            // в качестве самого первого сообщения специальное сообщение - HandshakeRequest.
            // Это свойство устанавливает допустимое время таймаута, которое может пройти
            // до получения от клиента первого сообщения об установки соединения.
            // Если в течение этого периода клиент не отправит первое сообщение,
            // то подключение закрывается. По умолчанию равно 15 секунд.

            // KeepAliveInterval: если в течение этого периода сервер не отправит никаких сообшений,
            // то автоматически отправляется ping - сообщение для поддержания подключения открытым.
            // При изменении этого свойства Microsoft рекомендует изменить на стророне клиента
            // параметр serverTimeoutInMilliseconds(клиент javascript)/ ServerTimeout(клиент.NET),
            // которое рекомендуется устанавливать в два раза больше, чем KeepAliveInterval.
            // По умолчанию равно 15 секунд.

            // SupportedProtocols определяет поддерживаемые протоколы. По умолчанию поддерживаются все протоколы.

            // EnableDetailedErrors при значении true возвращает клиенту детальное описание
            // возникшей ошибки(при ее возникновении). Поскольку подобные сообщения могут содержать
            // критически важную для безопасности информацию, то по умолчанию имеет значение false.

            // StreamBufferCapacity определяет максимальный размер буфера для входящего потока клиента.
            // По умолчанию равно 10.

            // MaximumReceiveMessageSize определяет максимальный размер для входящего сообщения.
            // По умолчанию -32 кб

            // MaximumParallelInvocationsPerClient определяет максимальное количество методов хаба,
            // которые клиент может вызвать параллельно.По умолчанию равно 1.



            //builder.Services.AddSignalR( hubOptions =>  // Глобальная настройка хабов
            //{
            //    hubOptions.EnableDetailedErrors = true; // при значении true возвращает клиенту детальное описание возникшей ошибки
            //                                            // (при ее возникновении). Поскольку подобные сообщения могут содержать
            //                                            // критически важную для безопасности информацию, то по умолчанию имеет значение false.
            //    hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1); // если в течение этого периода сервер не отправит никаких сообшений,
            //                                                            // то автоматически отправляется ping-сообщение для поддержания подключения открытым. 
            //});


            builder.Services.AddSignalR().AddHubOptions<ChatHub>(options => // Настройка только для хаба ChatHub:
            {
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(600);  // я добавил ожидание 600 сек
                options.HandshakeTimeout = TimeSpan.FromSeconds(600);       // я добавил ожидание 600 сек

                options.EnableDetailedErrors = true;
                options.KeepAliveInterval = System.TimeSpan.FromMinutes(1);
            });


            //builder.Services.AddSignalR( hubOptions => // комбинировать о ба вида настроек:
            //{ 
            //    hubOptions.EnableDetailedErrors = true; 
            //    hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1); 
            //}).AddHubOptions<ChatHub>(options => 
            //{
            //    options.EnableDetailedErrors = false; 
            //    options.KeepAliveInterval = TimeSpan.FromMinutes(5); 
            //});

            // Настройки для отдельного хаба переопределяют глобальные настройки.

            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();


            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Чтобы сервер мог сопоставить запросы с определенным хабом вызывается метод MapHub(). 
            /// С помощью метода MapHub() можно сопоставить URL с хабом. В данном случае все запросы 
            /// по адресу "/chat" будут обрабатываться хабом ChatHub.
            /// 
            // Перегрузка метода MapHub дополнительно принимает делегат с параметром типа HttpConnectionDispatcherOptions,
            // благодаря чему мы можем настроить различные настройки подключения:

            // ApplicationMaxBufferSize: максимальный размер буфера в байтах, в который сервер помещает получаемые от клиента данные.
            // По умолчанию равно 64 килобайта.

            // AuthorizationData: представляет список(объект IList) объектов IAuthorizeData, которые определяют,
            // авторизован ли клиент для подключения к хабу.

            // TransportMaxBufferSize максимальный размер буфера в байтах, в который сервер помещает данные для отправки клиенту.
            // По умолчанию равно 64 килобайта.

            // MinimumProtocolVersion: минимальная версия протокола. Применяется, чтобы отсеить клиентов определенных версий.

            // Transports представляет битовую маску из значений перечисления HttpTransportType,
            // которая устанавливает допустимые типы транспорта.По умолчанию применяются все типы транспорта.

            // LongPolling представляет объект LongPollingOptions, который настраивает транспорт LongPolling.
            // Этот класс имеет только одно свойство PollTimeout, которое устанавливает периодичность опроса.
            // По умолчанию равно 90 секунд.

            // WebSockets представляет объект Microsoft.AspNetCore.Http.Connections.WebSocketOptions,
            // который настраивает транспорт WebSocket. У данного объекта можно установить два свойства:

            // CloseTimeout: временный интервал после закрытия сервера, в течение которого клиент должен закрыть подключение.
            // Если клиенту не удастся закрыть подключение, то соединение с сервером автоматически завершается.

            // SubProtocolSelector: делегат, который устанавливает заголовок Sec-WebSocket - Protocol.


            //app.MapHub<ChatHub>("/chat", options => // Применим некоторые свойства:
            //{ 
            //    options.ApplicationMaxBufferSize = 128; 
            //    options.TransportMaxBufferSize = 128; 
            //    options.LongPolling.PollTimeout = TimeSpan.FromMinutes(1); 
            //    options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets; 
            //});




            app.MapHub<ChatHub>("/chat");   // устанавливаем маршруты для хаба ChatHub.
                                            // Mетод MapHub позволяет связать запросы и класс хаба.
                                            // В данном случае он устанавливает класс ChatHub
                                            // в качестве обработчика запросов по пути "/chat".
                                            // То есть, чтобы обратиться к хабу,
                                            // строка запроса должна иметь вид типа "https://localhost:5000/chat".

            app.Run();
        }
    }
}
