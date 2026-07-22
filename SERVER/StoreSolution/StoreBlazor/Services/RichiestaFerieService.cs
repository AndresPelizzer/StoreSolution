using Azure;
using StoreShared.Interfaces;
using StoreShared.Models.StoreDb;
using System.Net.Http.Json;
using System.Net.WebSockets;

namespace StoreBlazor.Services
{
    public class RichiestaFerieService : IRichiesteFerieService
    {

        private readonly HttpClient _http;

        public RichiestaFerieService(HttpClient http)
        {
             _http = http;
        }

        public async Task<RichiestaFerie> AddFeria(RichiestaFerie feria)
        {
            var response= await _http.PostAsJsonAsync("api/RichiesteFerie", feria);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RichiestaFerie>();
            }
            return null!;
        }

        public async Task DeleteFeria(int id)
        {
            await _http.DeleteAsync($"api/RichiestaFerie/{id}");
        }

        public async Task<RichiestaFerie> GetFeria(int id)
        {
           var feria= await _http.GetFromJsonAsync<RichiestaFerie>($"api/RichiesteFerie/{id}");
            return feria!;

        }

        public async Task<List<RichiestaFerie>> GetFerie()
        {
            return await _http.GetFromJsonAsync<List<RichiestaFerie>>($"api/RichiesteFerie");
        }

        public async Task<RichiestaFerie?> UpdateFeria(int id, RichiestaFerie feria)
        {
            var response = await _http.PutAsJsonAsync($"api/RichiesteFerie/{id}", feria);
            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<RichiestaFerie>();
            }
            else
            {
                return null;
            }
        }
        public async Task<List<RichiestaFerie>> GetFerieDipendente(int id)
        {
            var ferie = await _http.GetFromJsonAsync<List<RichiestaFerie>>($"api/RichiesteFerie/dipendente/{id}");
            return ferie ?? new List<RichiestaFerie>();
        }

        public async Task<bool> AggiornaStato(int id, string stato)
        {
            var response = await _http.PutAsJsonAsync($"api/RichiesteFerie/{id}/stato", stato);
            return response.IsSuccessStatusCode;
        }
    }
}
