using Microsoft.AspNetCore.SignalR;

namespace SignalRSeminer18Ekim2023.Hubs
{
    public class SohbetHub : Hub
    {
        public async Task MesajGonder(string kullanici, string mesaj)
        {
            await Clients.All.SendAsync("YeniMesaj", kullanici, mesaj);
        }

        public async Task KonumGonder(string renk, int x, int y)
        {
            await Clients.All.SendAsync("KonumAlindi", Context.ConnectionId, renk, x, y);
        }
    }
}
