using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.FileProviders;
using Microsoft.JSInterop;
using StoreBlazor.Services;
using StoreShared.Interfaces;
using StoreShared.Models;

namespace StoreBlazor.Pages
{
    public partial class Login
    {
        Credenziali credenziali = new Credenziali();

        [Inject]
        public IAuthService? AuthService { get; set; }

        [Inject]
        public AuthState? AuthState { get; set; }

        [Inject]
        public NavigationManager? Navigation {  get; set; }

        

        string? errore = null;
        public async Task login()
        {
            var risposta=await AuthService!.Login(credenziali);
            if (risposta == null)
            {
                errore = "Credenziali errate";
                
                

            }
            else
            {
                errore = null;
                AuthState!.Token = risposta.Token;
                AuthState.Ruolo = risposta.Ruolo;
                AuthState.CodiceUtente = risposta.CodiceUtente;
                
                
                

            }





          }
    }
}
