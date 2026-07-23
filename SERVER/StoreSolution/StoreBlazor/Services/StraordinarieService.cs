using StoreShared.Interfaces;
using StoreShared.Models.StoreDb;
using System.Net.Http.Json;

namespace StoreBlazor.Services
{
    public class StraordinarieService : IStraordinarieService
    {

        private readonly HttpClient _http;

        public StraordinarieService(HttpClient http)
        {
            _http= http;
        }

        public async Task DeleteStraordinaria(int id)
        {
            await _http.DeleteAsync($"api/Straordinarie/{id}");
        }

        public async Task<Straordinaria?> GetStraordinaria(int id)
        {
            return await _http.GetFromJsonAsync<Straordinaria>($"api/Straordinarie/{id}");
          
            

        }

        public async Task<List<Straordinaria>?> GetStraordinarie()
        {
            return  await _http.GetFromJsonAsync<List<Straordinaria>>($"api/Straordinarie");
            
            
        }

        public async Task<Straordinaria> PostStraordinaria(Straordinaria straordinaria)
        {

            var response = await _http.PostAsJsonAsync("api/Straordinarie", straordinaria);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadFromJsonAsync<Straordinaria>();
                return str!;
            }
            else
            {
                return null!;
            }
        }

        public async Task<Straordinaria> PutStraordinaria(Straordinaria straordinaria, int id)
        {
            var response = await _http.PutAsJsonAsync($"api/Straordinarie/{id}", straordinaria);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadFromJsonAsync<Straordinaria>();
                return str!;
            }
            else
            {
                return null!;
            }
        }
    }
}
