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

        Utente? utente = new Utente();

        [Inject]
        public NavigationManager? Navigation {  get; set; }

        
        public List<Utente>? utenti = new List<Utente>();

        [Inject]
        public IUtentiService? UtentiService { get; set; }
        

        string? errore = null;
        string? successo = null;

        protected override async Task OnInitializedAsync()
        {
            utenti = await UtentiService!.GetUtenti();

            
        }
        public async Task login()
        {
            var risposta=await AuthService!.Login(credenziali);
            if (risposta == null)
            {
                errore = "Credenziali errate";
                successo = null;
                
                

            }
            else
            {
                errore = null;
                AuthState!.Token = risposta.Token;
                AuthState.Ruolo = risposta.Ruolo;
                AuthState.CodiceUtente = risposta.CodiceUtente;
                successo = "Login avvenuto con successo!";



                if (AuthState.Ruolo == "Admin")
                {
                    Navigation!.NavigateTo("admin/home");
                }
                else if (AuthState.Ruolo == "Dipendente")
                {
                    utente = utenti!.FirstOrDefault(u => u.Codice == risposta.CodiceUtente);



                    Navigation!.NavigateTo($"dipendente/{utente!.CodiceDipendente}/home");
                }
               
                

            }



            

          }
    }
}
