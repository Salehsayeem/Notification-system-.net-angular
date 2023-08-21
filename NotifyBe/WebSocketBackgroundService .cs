

using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace NotifyBe
{
    // Example of scheduling notifications in a background service
    public class WebSocketBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public WebSocketBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var webSocketHandler = scope.ServiceProvider.GetRequiredService<WebSocketHandler>();
                    var message = "Sayeem: " + DateTime.Now.ToString();
                    await webSocketHandler.SendNotificationsAsync(JsonSerializer.Serialize(message));
                    Console.WriteLine(message);
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }


}
