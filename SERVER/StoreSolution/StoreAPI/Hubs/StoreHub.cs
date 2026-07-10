using Microsoft.AspNetCore.SignalR;

namespace StoreAPI.Hubs;

public class StoreHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task NotificaClienti()
    {
        await Clients.All.SendAsync("AggiornaClienti");
    }

    public async Task NotificaRichieste()
    {
        await Clients.All.SendAsync("AggiornaRichieste");
    }

    public async Task NotificaDipendenti()
    {
        await Clients.All.SendAsync("AggiornaDipendenti");
    }
}

