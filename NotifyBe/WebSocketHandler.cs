using System.Net.WebSockets;
using System.Text;

namespace NotifyBe
{
    public class WebSocketHandler
    {
        private WebSocket _webSocket;

        public WebSocket WebSocketInstance => _webSocket; // Getter for WebSocket instance

        public async Task HandleWebSocketAsync(HttpContext context, WebSocket webSocket)
        {
            _webSocket = webSocket;
            await ReceiveLoop();
        }

        public async Task SendNotificationsAsync(string message)
        {
            if (WebSocketInstance != null && WebSocketInstance.State == WebSocketState.Open)
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                await WebSocketInstance.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private async Task ReceiveLoop()
        {
            var buffer = new byte[1024];
            while (WebSocketInstance != null && WebSocketInstance.State == WebSocketState.Open)
            {
                var result = await WebSocketInstance.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await WebSocketInstance.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
            }
        }
    }
}