using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace ClassyAdsServer.Services
{
    public class WebSocketService
    {
        private readonly HttpListener _httpListener;
        private readonly ConcurrentDictionary<string, WebSocket> connectedClients;

        public void Start()
        {
            _httpListener.Start();
            Console.WriteLine("WebSocket server started.");

            AcceptWebSocketRequests();
        }

        private async void AcceptWebSocketRequests()
        {
            while (true)
            {
                var context = await _httpListener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    var webSocketContext = await context.AcceptWebSocketAsync(null);
                    HandleWebSocketConnection(webSocketContext.WebSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }
        }

        private async void HandleWebSocketConnection(WebSocket webSocket)
        {
            var buffer = new byte[1024];

            while (webSocket.State == WebSocketState.Open)
            {
                var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (receiveResult.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                    Console.WriteLine("Received message: " + message);

                    await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, receiveResult.Count), WebSocketMessageType.Text, receiveResult.EndOfMessage, CancellationToken.None);
                }
            }

            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            Console.WriteLine("WebSocket connection closed.");
        }
    }
}
