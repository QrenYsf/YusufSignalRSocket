using Microsoft.AspNetCore.SignalR;

namespace YusufSignalrSocket.Hubs
{
    public class PriceHub : Hub
    {
        public async Task SendPriceUpdate(string productId, decimal newPrice)
        {
            await Clients.All.SendAsync("PriceUpdated", productId, newPrice);
        }
    }
}

