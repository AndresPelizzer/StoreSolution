using Microsoft.AspNetCore.Components;
using StoreBlazor.Services;
using StoreShared.Interfaces;
using StoreShared.Models;
using StoreShared.Models.StoreDb;

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

        
        //public List<Utente>? utenti = new List<Utente>();

        [Inject]
        public IUtentiService? UtentiService { get; set; }

        [Inject]
        public IDipendentiService? DipendentiService { get; set; }

        [Inject]
        public IClientiService? ClientiService { get; set; }
        
        
        string? errore = null;
        string? successo = null;

        protected override async Task OnInitializedAsync()
        {
            //utenti = await UtentiService!.GetUtenti();            
        }

        public async Task login()
        {
            var risposta=await AuthService!.Login(credenziali);
            if (risposta == null || string.IsNullOrEmpty(risposta.Token))
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
                AuthState.IsCapoArea = risposta.IsCapoArea ?? false;
                successo = "Login avvenuto con successo!";

                int id = risposta.CodiceUtente ?? 0;

                if (AuthState.Ruolo == "Admin")
                {
                    Navigation!.NavigateTo("admin/home");
                }
                else if (AuthState.Ruolo == "dipendente")
                {

                    //utente = utenti!.FirstOrDefault(u => u.Codice == risposta.CodiceUtente);
                    utente = await UtentiService!.GetUtente(id);

                    var dipendente = await DipendentiService!.GetDipendente(utente!.CodiceDipendente!.Value);

                    if (AuthState.IsCapoArea == true)
                    {
                        Navigation!.NavigateTo($"capoarea/{dipendente!.CodiceAreaAppl}/home");
                    }
                    else
                    {

                        Navigation!.NavigateTo($"dipendente/{utente!.CodiceDipendente}/home");

                    }
                }
                else if (AuthState.Ruolo == "cliente")
                {
                    utente = await UtentiService!.GetUtente(id);

                    //utente = utenti!.FirstOrDefault(u => u.Codice == risposta.CodiceUtente);
                    var cliente = await ClientiService!.GetCliente(utente!.CodiceCliente!.Value);
                    if (cliente != null)
                    {
                        Navigation!.NavigateTo($"cliente/{utente!.CodiceCliente}/home");
                    }

                }
                
               
                

            }



            

          }
    }
}
