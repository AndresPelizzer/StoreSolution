using StoreBlazor.Pages;
using StoreShared.Interfaces;
using StoreShared.Models.StoreDb;
using System.Net.Http.Json;

namespace StoreBlazor.Services;

public class AttDipService : IAttDipService
{

    private readonly HttpClient _http;

    public AttDipService(HttpClient http)
    {
        _http = http; 
    }
    public async Task<AttDip?> AddAttDip(AttDip attDip)
    {
        var response = await _http.PostAsJsonAsync("api/AttDip", attDip);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AttDip>();
        }
        else
        {
            var errore = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Errore AddAttDip: {errore}");
            return null!;
        }


    }

    public async Task DeleteAttDip(int id)
    {

        await _http.DeleteAsync($"api/AttDip/{id}");

    }

    public async Task<AttDip> GetAttDip(int id)
    {
        var response= await _http.GetFromJsonAsync<AttDip>($"api/AttDip/{id}");
        return response!;
    }

    public async Task<List<AttDip>> GetAttsDip()
    {

        var response=await _http.GetFromJsonAsync<List<AttDip>>("api/AttDip");
        return response!;
        
    }

    public async Task<AttDip?> UpdateAttDip(AttDip AttDip, int id)
    {
        var response = await _http.PutAsJsonAsync<AttDip>($"api/AttDip/{id}", AttDip);
        if (response != null)
        {
            return await response.Content.ReadFromJsonAsync<AttDip>();
        }
        else
        {
            return null!;
        }
    }

}

