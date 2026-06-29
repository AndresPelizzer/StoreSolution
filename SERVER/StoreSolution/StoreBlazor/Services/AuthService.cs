using StoreShared.Interfaces;
using StoreShared.Models;
using System.Net.Http.Json;




namespace StoreBlazor.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }


        public async Task<LoginResponse?> Login(Credenziali? credenziali)
        {

            var response = await _http.PostAsJsonAsync<Credenziali?>("api/auth/login", credenziali);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<LoginResponse>();
          
        }
    }

    


    
        
}
