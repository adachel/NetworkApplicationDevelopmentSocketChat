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
            // builder.Services.AddSignalR(); // ��������� � ���������� ������� SignalR

            // ����� HubOptions ���������� ��� �������:
            // ClientTimeoutInterval. ���������� �����, � ������� �������� ������ ������ ��������� ������� ���������.
            // ���� � ������� ������� ������� ������� �������� �� ������� �� ������ �� ������, �� ������ ��������� ����������.
            // �� ��������� ����� 30 ������.

            // HandshakeTimeout.����� ����������� � ������� ������ ������ ��������� �������
            // � �������� ������ ������� ��������� ����������� ��������� - HandshakeRequest.
            // ��� �������� ������������� ���������� ����� ��������, ������� ����� ������
            // �� ��������� �� ������� ������� ��������� �� ��������� ����������.
            // ���� � ������� ����� ������� ������ �� �������� ������ ���������,
            // �� ����������� �����������. �� ��������� ����� 15 ������.

            // KeepAliveInterval: ���� � ������� ����� ������� ������ �� �������� ������� ���������,
            // �� ������������� ������������ ping - ��������� ��� ����������� ����������� ��������.
            // ��� ��������� ����� �������� Microsoft ����������� �������� �� �������� �������
            // �������� serverTimeoutInMilliseconds(������ javascript)/ ServerTimeout(������.NET),
            // ������� ������������� ������������� � ��� ���� ������, ��� KeepAliveInterval.
            // �� ��������� ����� 15 ������.

            // SupportedProtocols ���������� �������������� ���������. �� ��������� �������������� ��� ���������.

            // EnableDetailedErrors ��� �������� true ���������� ������� ��������� ��������
            // ��������� ������(��� �� �������������). ��������� �������� ��������� ����� ���������
            // ���������� ������ ��� ������������ ����������, �� �� ��������� ����� �������� false.

            // StreamBufferCapacity ���������� ������������ ������ ������ ��� ��������� ������ �������.
            // �� ��������� ����� 10.

            // MaximumReceiveMessageSize ���������� ������������ ������ ��� ��������� ���������.
            // �� ��������� -32 ��

            // MaximumParallelInvocationsPerClient ���������� ������������ ���������� ������� ����,
            // ������� ������ ����� ������� �����������.�� ��������� ����� 1.



            //builder.Services.AddSignalR( hubOptions =>  // ���������� ��������� �����
            //{
            //    hubOptions.EnableDetailedErrors = true; // ��� �������� true ���������� ������� ��������� �������� ��������� ������
            //                                            // (��� �� �������������). ��������� �������� ��������� ����� ���������
            //                                            // ���������� ������ ��� ������������ ����������, �� �� ��������� ����� �������� false.
            //    hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1); // ���� � ������� ����� ������� ������ �� �������� ������� ���������,
            //                                                            // �� ������������� ������������ ping-��������� ��� ����������� ����������� ��������. 
            //});


            builder.Services.AddSignalR().AddHubOptions<ChatHub>(options => // ��������� ������ ��� ���� ChatHub:
            {
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(600);  // � ������� �������� 600 ���
                options.HandshakeTimeout = TimeSpan.FromSeconds(600);       // � ������� �������� 600 ���

                options.EnableDetailedErrors = true;
                options.KeepAliveInterval = System.TimeSpan.FromMinutes(1);
            });


            //builder.Services.AddSignalR( hubOptions => // ������������� � �� ���� ��������:
            //{ 
            //    hubOptions.EnableDetailedErrors = true; 
            //    hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1); 
            //}).AddHubOptions<ChatHub>(options => 
            //{
            //    options.EnableDetailedErrors = false; 
            //    options.KeepAliveInterval = TimeSpan.FromMinutes(5); 
            //});

            // ��������� ��� ���������� ���� �������������� ���������� ���������.

            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();


            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            /// ����� ������ ��� ����������� ������� � ������������ ����� ���������� ����� MapHub(). 
            /// � ������� ������ MapHub() ����� ����������� URL � �����. � ������ ������ ��� ������� 
            /// �� ������ "/chat" ����� �������������� ����� ChatHub.
            /// 
            // ���������� ������ MapHub ������������� ��������� ������� � ���������� ���� HttpConnectionDispatcherOptions,
            // ��������� ���� �� ����� ��������� ��������� ��������� �����������:

            // ApplicationMaxBufferSize: ������������ ������ ������ � ������, � ������� ������ �������� ���������� �� ������� ������.
            // �� ��������� ����� 64 ���������.

            // AuthorizationData: ������������ ������(������ IList) �������� IAuthorizeData, ������� ����������,
            // ����������� �� ������ ��� ����������� � ����.

            // TransportMaxBufferSize ������������ ������ ������ � ������, � ������� ������ �������� ������ ��� �������� �������.
            // �� ��������� ����� 64 ���������.

            // MinimumProtocolVersion: ����������� ������ ���������. �����������, ����� ������� �������� ������������ ������.

            // Transports ������������ ������� ����� �� �������� ������������ HttpTransportType,
            // ������� ������������� ���������� ���� ����������.�� ��������� ����������� ��� ���� ����������.

            // LongPolling ������������ ������ LongPollingOptions, ������� ����������� ��������� LongPolling.
            // ���� ����� ����� ������ ���� �������� PollTimeout, ������� ������������� ������������� ������.
            // �� ��������� ����� 90 ������.

            // WebSockets ������������ ������ Microsoft.AspNetCore.Http.Connections.WebSocketOptions,
            // ������� ����������� ��������� WebSocket. � ������� ������� ����� ���������� ��� ��������:

            // CloseTimeout: ��������� �������� ����� �������� �������, � ������� �������� ������ ������ ������� �����������.
            // ���� ������� �� ������� ������� �����������, �� ���������� � �������� ������������� �����������.

            // SubProtocolSelector: �������, ������� ������������� ��������� Sec-WebSocket - Protocol.


            //app.MapHub<ChatHub>("/chat", options => // �������� ��������� ��������:
            //{ 
            //    options.ApplicationMaxBufferSize = 128; 
            //    options.TransportMaxBufferSize = 128; 
            //    options.LongPolling.PollTimeout = TimeSpan.FromMinutes(1); 
            //    options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets; 
            //});




            app.MapHub<ChatHub>("/chat");   // ������������� �������� ��� ���� ChatHub.
                                            // M���� MapHub ��������� ������� ������� � ����� ����.
                                            // � ������ ������ �� ������������� ����� ChatHub
                                            // � �������� ����������� �������� �� ���� "/chat".
                                            // �� ����, ����� ���������� � ����,
                                            // ������ ������� ������ ����� ��� ���� "https://localhost:5000/chat".

            app.Run();
        }
    }
}
