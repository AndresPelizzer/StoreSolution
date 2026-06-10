using BlazorAppTest.Interfaces;
using BlazorAppTest.Models;
using System.Net.Http.Json;

namespace BlazorAppTest.Services
{
    public class GiocatoriService : IGiocatori
    {

        private readonly HttpClient _http;
        private readonly string _apiUrl = "http://localhost:3000/users";
        
           
      public GiocatoriService(HttpClient http)
        {
            _http = http;
        }


        public async Task<bool> AddGiocatore(Giocatore item)
        {
            try
            {


                var risposta = await _http.PostAsJsonAsync(_apiUrl, item);

                return risposta.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteGiocatore(string id)
        {
            try
            {

                var risposta=await _http.DeleteAsync($"{_apiUrl}/{id}");

                return risposta.IsSuccessStatusCode;
            }
            catch  {
                return false;
               }

        }

        public async Task<Giocatore>GetGiocatore(string Codice)
        {
            return await _http.GetFromJsonAsync<Giocatore>($"{_apiUrl}/{Codice}");
        }

        public async Task<List<Giocatore>> GetGiocatori()
        {
            try
            {

                return await _http.GetFromJsonAsync<List<Giocatore>>($"{_apiUrl}");
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> UpdateGiocatore(string id, Giocatore newValues)
        {
            try
            {
                var risposta = await _http.PutAsJsonAsync($"{_apiUrl}/{id}", newValues);
                return risposta.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        


        }
        













    }

