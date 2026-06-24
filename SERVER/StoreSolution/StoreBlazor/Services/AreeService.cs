
using StoreShared.Interfaces;
using StoreShared.Models;
using System.Net.Http.Json;

namespace StoreBlazor.Services
{
    public class AreeService : IAreeService
    {

        private readonly HttpClient _http;

        public AreeService(HttpClient http)
        {
            _http = http;
        }
        public async Task<Area?> AddArea(Area Area)
        {
            var response = await _http.PostAsJsonAsync<Area?>("api/Aree", Area);
            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<Area>();
            }
            else
            {
                return null;
            }



        }

        public async Task DeleteArea(int id)
        {
            await _http.DeleteAsync($"api/Aree/{id}");

        }

        public async Task<Area?> GetArea(int id)
        {
            return await _http.GetFromJsonAsync<Area>($"api/Aree/{id}");
        }

        public async Task<List<Area>?> GetAree()
        {
            return await _http.GetFromJsonAsync<List<Area>>("api/Aree");
        }

        public async Task<Area?> UpdateArea(Area Area, int id)
        {
            var response = await _http.PutAsJsonAsync<Area>($"api/Aree/{id}", Area);
            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<Area>();
            }
            else
            {
                return null;
            }
        }
    }
}
