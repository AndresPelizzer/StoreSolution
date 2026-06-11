using BlazorAppTest.Interfaces;
using BlazorAppTest.Models;
using BlazorAppTest.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;

namespace BlazorAppTest.Pages
{
    public partial class Giocatori
    {
        [Inject] CalcoliService CalcService { get; set; } = null;
        [Inject] IGiocatori gService { get; set; } = null;
        [Inject] NavigationManager Navigation { get; set; } = null;
        [Inject] IJSRuntime JS { get; set; }

        Giocatore giocatoreSelezionato { get; set; } = null;
        Giocatore giocatoreIns { get; set; } = new();

        bool mostraModifica = false;
        List<Giocatore> giocatori = new List<Giocatore>();
        public double risultato { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            await leggiDatiAsync();
        }

        private async Task leggiDatiAsync()
        {
            var dati = await gService.GetGiocatori();
            if (dati != null)
            {
                
                giocatori = dati.OrderBy(g => g.Codice).ToList();
            }
        }

        async Task aggiungi()
        {   


            var nuovo = new Giocatore
            {
                Name = giocatoreIns.Name,
                Username = giocatoreIns.Username,
                Email = giocatoreIns.Email
            };

            await gService.AddGiocatore(nuovo);
            await leggiDatiAsync();
            giocatoreIns = new();

           
        }

        async Task elimina(Giocatore g)
        {
            bool conferma = await JS.InvokeAsync<bool>("confirm", $"Sei sicuro di voler eliminare {g.Name} {g.Username}?");
            if (conferma)
            {
                await gService.DeleteGiocatore(g.Codice);
                await leggiDatiAsync();
            }
        }

        void modifica(Giocatore g)
        {
            giocatoreSelezionato = g;
            giocatoreIns.Name = g.Name;
            giocatoreIns.Username = g.Username;
            giocatoreIns.Email = g.Email;
            mostraModifica = true;
        }

        void annulla(Giocatore g)
        {
            g.Name = giocatoreIns.Name;
            g.Username = giocatoreIns.Username;
            g.Email = giocatoreIns.Email;
            mostraModifica = false;
        }

      

        void mostraDettagli(int codice)
        {
            Navigation.NavigateTo($"/giocatori/{codice}");

        }
    }
}