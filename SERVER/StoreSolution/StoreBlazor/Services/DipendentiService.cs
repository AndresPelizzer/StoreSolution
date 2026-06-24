
using StoreShared.Interfaces;
using StoreShared.Models;
using System.Net.Http.Json;

namespace StoreBlazor.Services
{
    public class DipendentiService : IDipendentiService
    {

        private readonly HttpClient _http;

        public DipendentiService(HttpClient http)
        {
            _http = http;
        }
        public async Task<Dipendente?> AddDipendente(Dipendente Dipendente)
        {
            var response = await _http.PostAsJsonAsync<Dipendente?>("api/Dipendenti", Dipendente);
            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<Dipendente>();
            }
            else
            {
                return null;
            }



        }

        public async Task DeleteDipendente(int id)
        {
            await _http.DeleteAsync($"api/Dipendenti/{id}");

        }

        public async Task<Dipendente?> GetDipendente(int id)
        {
            return await _http.GetFromJsonAsync<Dipendente>($"api/Dipendenti/{id}");
        }

        public async Task<List<Dipendente>?> GetDipendenti()
        {
            return await _http.GetFromJsonAsync<List<Dipendente>>("api/Dipendenti");
        }

        public async Task<Dipendente?> UpdateDipendente(Dipendente Dipendente, int id)
        {
            var response = await _http.PutAsJsonAsync<Dipendente>($"api/Dipendenti/{id}", Dipendente);
            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<Dipendente>();
            }
            else
            {
                return null;
            }
        }
    }
}
