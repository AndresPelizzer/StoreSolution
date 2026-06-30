
using StoreShared.Interfaces;
using StoreShared.Models;
using System.Net.Http.Json;

namespace StoreBlazor.Services
{
    public class ClientiService : IClientiService
    {

        private readonly HttpClient _http;

        public ClientiService(HttpClient http)
        {
            _http = http;
        }
        public async Task<Cliente?> AddCliente(Cliente cliente)
        {
            var response =  await _http.PostAsJsonAsync<Cliente?>("api/Clienti", cliente);
            if(response != null)
            {
                return await response.Content.ReadFromJsonAsync<Cliente>();
            }
            else
            {
                return null;
            }
            
            
        
        }

        public async Task DeleteCliente(int id)
        {
           await _http.DeleteAsync($"api/Clienti/{id}");
           
        }

        public async Task<Cliente?> GetCliente(int id)
        {
            return await _http.GetFromJsonAsync<Cliente>($"api/Clienti/{id}");
        }

        public async Task<List<Cliente>?> GetClienti()
        {
            return await _http.GetFromJsonAsync<List<Cliente>>("api/Clienti");
        }

        public async Task<Cliente?> UpdateCliente(Cliente Cliente, int id)
        {
            var response = await _http.PutAsJsonAsync<Cliente>($"api/Clienti/{id}", Cliente);
            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<Cliente>();
            }
            else
            {
                return null;
            }
        }

        public async Task<ImportResult?> ImportClienti(Stream fileStream, string fileName)
        {
            using var content = new MultipartFormDataContent();
            using var fileContent = new StreamContent(fileStream);
            content.Add(fileContent, "file", fileName);

            var response = await _http.PostAsync("api/Clienti/import", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ImportResult>();
            }
            return null;
        }


        public async Task<ImportResult?> ImportClientiCsv(Stream fileStream, string fileName)
        {
            using var content = new MultipartFormDataContent();
            using var fileContent = new StreamContent(fileStream);
            content.Add(fileContent, "file", fileName);

            var response = await _http.PostAsync("api/Clienti/import-csv", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ImportResult>();
            }
            return null;
        }



    }
}
