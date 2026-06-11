using BlazorAppTest.Interfaces;
using BlazorAppTest.Models;
using System.Net.Http.Json;

namespace BlazorAppTest.Services
{
    public class GiocatoriService : IGiocatori
    {
        private readonly HttpClient _http;
        
        private readonly string _apiUrl = "https://localhost:7266/api/Giocatori";

        public GiocatoriService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse> AddGiocatore(Giocatore item)
        {
            try
            {

                // 
                if(string.IsNullOrEmpty(item.Email)) {
                    return new ApiResponse
                    {
                        success = false,
                        message = "Email NON valida!!!"
                    };

                }


                var risposta = await _http.PostAsJsonAsync(_apiUrl, item);
                if (risposta.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        success = true,
                        message = "Operazione riuscita"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        success = false,
                        message = "Operazione non riuscita"
                    };
                }

            }
            catch(Exception ex)
            {
                return new ApiResponse
                {
                    success = false,
                    message = ex.Message
                };
            }
        }

        
        public async Task<bool> DeleteGiocatore(int id)
        {
            try
            {
                var risposta = await _http.DeleteAsync($"{_apiUrl}/{id}");
                return risposta.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }


        public async Task<Giocatore> GetGiocatore(int Codice)
        {
            return await _http.GetFromJsonAsync<Giocatore>($"{_apiUrl}/{Codice}");
        }

        public async Task<List<Giocatore>> GetGiocatori()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<Giocatore>>(_apiUrl);
            }
            catch (Exception)
            {
                return null;
            }
        }

      
        public async Task<ApiResponse> UpdateGiocatore(int id, Giocatore newValues)
        {
            try
            {
                var risposta = await _http.PutAsJsonAsync($"{_apiUrl}/{id}", newValues);
                if (risposta.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        success = true,
                        message = "Operazione riuscita"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        success = false,
                        message = "Operazione non riuscita"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}