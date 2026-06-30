
using StoreShared.Interfaces;
using StoreShared.Models;
using System.Net.Http.Json;

namespace StoreBlazor.Services
{
    public class UtentiService : IUtentiService
    {

        private readonly HttpClient _http;

        public UtentiService(HttpClient http)
        {
            _http = http;
        }
        public async Task<Utente?> AddUtente(Utente Utente)
        {
            var response = await _http.PostAsJsonAsync<Utente?>("api/Utenti", Utente);
            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<Utente>();
            }
            else
            {
                return null;
            }



        }

        public async Task DeleteUtente(int id)
        {
            await _http.DeleteAsync($"api/Utenti/{id}");

        }

        public async Task<Utente?> GetUtente(int id)
        {
            return await _http.GetFromJsonAsync<Utente>($"api/Utenti/{id}");
        }

        public async Task<List<Utente>?> GetUtenti()
        {
            return await _http.GetFromJsonAsync<List<Utente>>("api/Utenti");
        }

        public async Task<Utente?> UpdateUtente(Utente Utente, int id)
        {
            var response = await _http.PutAsJsonAsync<Utente>($"api/Utenti/{id}", Utente);
            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<Utente>();
            }
            else
            {
                return null;
            }
        }
    }
}
