using Azure;
using StoreShared.Models.StoreDb;
using System.Net.Http.Json;

namespace StoreBlazor.Services;

public class NotificheService
{

    private readonly HttpClient _http;

    public NotificheService(HttpClient http)
    {
        _http = http;
    }


    public async Task<List<Notifica>> GetNotifiche(int id)
    {
        var notifiche = await _http.GetFromJsonAsync<List<Notifica>>($"api/Notifiche/{id}");
        return notifiche ?? new List<Notifica>();
    }

    public async Task<List<Notifica>> GetNotificheCapoArea(int id)
    {
        var notifiche = await _http.GetFromJsonAsync<List<Notifica>>($"api/Notifiche/dipendente/{id}");
        return notifiche ?? new List<Notifica>();
    }


    public async Task<bool> AddNotifica(Notifica notifica)
    {
        var response = await _http.PostAsJsonAsync("api/Notifiche", notifica);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateNotifica(int id)
    {
        var response = await _http.PutAsJsonAsync($"api/Notifiche/{id}/letta", new { });
        return response.IsSuccessStatusCode;
    }
}

