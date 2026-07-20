using StoreShared.Interfaces;
using StoreShared.Models.StoreDb;
using System.Net.Http.Json;

namespace StoreBlazor.Services
{
    public class SedeService : ISedeService
    {
        private readonly HttpClient _http;

        public SedeService(HttpClient http)
        {
            _http = http;
        }
        
        public async Task<Sede> AddSede(Sede sede)
        {

           
               var response= await _http.PostAsJsonAsync("api/Sedi",sede);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Sede>();
            }
            else
            {
                var errore= await response.Content.ReadAsStringAsync();
                return null!;
            }
            
            

            
        }

        public async Task DeleteSede(int id)
        {
            await _http.DeleteAsync($"api/Sedi/{id}");
                    }

        public async Task<Sede?> GetSede(int id)
        {
           return await _http.GetFromJsonAsync<Sede>($"api/Sedi/{id}");
          

        }

        public async Task<List<Sede>?> GetSedi()
        {
            return await _http.GetFromJsonAsync<List<Sede>>("api/Sedi");    }

        public async Task<Sede> UpdateSede(int id, Sede sede)
        {
            var response= await _http.PutAsJsonAsync<Sede>($"api/Sedi/{id}", sede);

            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<Sede>();
                
            }
            else
            {
                return null!;
            }
        }

       
    }
}
