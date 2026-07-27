using StoreShared.Interfaces;
using StoreShared.Models.StoreDb;
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
            try
            {
                return await _http.GetFromJsonAsync<Utente>($"api/Utenti/{id}");
            }
            catch (Exception ex) { 
            
            string msg = ex.Message;
                return null!;
            }
            
        }

        public async Task<List<Utente>?> GetUtenti()
        {

            try
            {
                return await _http.GetFromJsonAsync<List<Utente>>("api/Utenti");
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
                return null!;
            }




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
