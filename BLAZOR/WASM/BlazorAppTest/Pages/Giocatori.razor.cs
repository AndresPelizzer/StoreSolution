using BlazorAppTest.Interfaces;
using BlazorAppTest.Models;
using BlazorAppTest.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorAppTest.Pages;

    public partial class Giocatori
    {

        [Inject] CalcoliService CalcService { get; set; } = null;
        [Inject] IGiocatori gService { get; set; } = null;

    [Inject] NavigationManager Navigation { get; set; } = null;

   


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
            giocatori = await gService.GetGiocatori();



        }
        




            async Task aggiungi()
        {


        //Navigation.NavigateTo("giocatori/8");
        //return;




            var nuovo = new Giocatore
            {
                
                name = giocatoreIns.name,
                username = giocatoreIns.username,
                email = giocatoreIns.email

            };


            await gService.AddGiocatore(nuovo);
            await leggiDatiAsync();
            giocatoreIns = new();



            this.risultato = this.CalcService.Somma(1, 3);

        }



        async Task elimina(Giocatore g)

        {

            bool conferma = await JS.InvokeAsync<bool>("confirm", $"Sei sicuro di voler eliminare {g.name} {g.username}?");
            if (conferma)
            {
                await gService.DeleteGiocatore(g.Codice);
                await leggiDatiAsync();
            }
        }
        

        void modifica(Giocatore g)
        {
            giocatoreSelezionato = g;

            giocatoreIns.name = g.name;
            giocatoreIns.username = g.username;
            giocatoreIns.email = g.email;


            mostraModifica = true;

        }
        void annulla(Giocatore g)
        {
            //int codice = g.Codice;
            //Giocatore unchanged= gService.GetGiocatore(codice);


            g.name = giocatoreIns.name;
            g.username = giocatoreIns.username;
            g.email = giocatoreIns.email;


            mostraModifica = true;
        }
        async Task salvaModifica(Giocatore giocatoreModificato)
        {
            //

            bool success=await gService.UpdateGiocatore(giocatoreModificato.Codice, giocatoreSelezionato);

            if (success)
            {
                await leggiDatiAsync();
                giocatoreIns = new();
                mostraModifica = false;

                await JS.InvokeVoidAsync("alert", "Hai salvato le modifiche con successo!");
            }
            else
            {
                await JS.InvokeVoidAsync("alert", "Salvataggio NON riuscito!");
            }

        }
        
    void mostraDettagli(string codice)
    {
        Navigation.NavigateTo($"/giocatori/{codice}");
    }

        
    
}




