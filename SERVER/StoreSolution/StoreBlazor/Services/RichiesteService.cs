
using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.AspNetCore.Components.Forms;
using StoreShared.Interfaces;
using StoreShared.Models.StoreDb;
using System.Net.Http.Json;


namespace StoreBlazor.Services
{
    public class RichiesteService : IRichiesteService
    {

        private readonly HttpClient _http;

        public RichiesteService(HttpClient http)
        {
            _http = http;
        }
        public async Task<Richiesta?> AddRichiesta(Richiesta Richiesta)
        {
            var response = await _http.PostAsJsonAsync<Richiesta?>("api/Richieste", Richiesta);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Richiesta>();
            }
            else
            {
                return null;
            }

        }

        public async Task DeleteRichiesta(int id)
        {
            await _http.DeleteAsync($"api/Richieste/{id}");

        }

        public async Task<Richiesta?> GetRichiesta(int id)
        {
            return await _http.GetFromJsonAsync<Richiesta>($"api/Richieste/{id}");
        }

        public async Task<List<Richiesta>?> GetRichieste()
        {
            return await _http.GetFromJsonAsync<List<Richiesta>>("api/Richieste");
        }
         
        public async Task<Richiesta?> UpdateRichiesta(Richiesta Richiesta, int id)
        {
            var response = await _http.PutAsJsonAsync<Richiesta>($"api/Richieste/{id}", Richiesta);
            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<Richiesta>();
            }
            else
            {
                return null;
            }
        }

        public async Task UploadAllegato(int id, IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); 
            content.Add(new StreamContent(stream), "file", file.Name);
            await _http.PutAsync($"richieste/{id}/allegato", content);
        }
    }
}
    


         



