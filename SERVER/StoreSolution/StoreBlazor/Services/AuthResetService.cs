using StoreShared.Models;
using System.Net.Http.Json;

namespace StoreBlazor.Services;

public class AuthResetService
{
    private readonly HttpClient _http;

    public AuthResetService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> AddRichiesta(RichiestaResetPassword richiesta)
    {
        var response = await _http.PostAsJsonAsync("api/AuthReset/richiedi-reset", richiesta);
        return response.IsSuccessStatusCode;
      
    }

    public async Task<bool> Conferma(ConfermaResetPassword conferma)
    {
        var response = await _http.PostAsJsonAsync("api/AuthReset/conferma-reset", conferma);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
