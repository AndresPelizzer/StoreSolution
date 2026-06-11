using BlazorAppTest.Interfaces;
using BlazorAppTest.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorAppTest.Pages
{
    public partial class PageGiocatore
    {
        [Parameter] public int codice { get; set; } = 0;
        [Inject] private NavigationManager Navigation { get; set; } = null!;
        [Inject] private IJSRuntime JS { get; set; } = null!;
        [Inject] private IGiocatori gService { get; set; } = null!;

        private Giocatore giocatoreIns { get; set; } = new();
        private List<Giocatore> giocatori { get; set; } = new List<Giocatore>();
        private bool mostraModifica { get; set; } = false;
        private Giocatore? giocatoreSelezionato { get; set; } = null;
        public Giocatore giocatore { get; set; } = new();

        
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

           
            await leggiDatiAsync();

            
            await Task.Delay(500);

            if (this.codice > 0)
            {
                giocatore = await gService.GetGiocatore(codice);
            }
            else
            {
                giocatore = new Giocatore();
            }
        }

        private async Task leggiDatiAsync()
        {
            giocatori = await gService.GetGiocatori();
        }


        private async Task salvaModifica(Giocatore giocatoreModificato)
        {
            ApiResponse response;

            if (codice == 0)
            {
               
                 response = await gService.AddGiocatore(giocatoreModificato);
            }
            else
            {

                 response  = await gService.UpdateGiocatore(giocatoreModificato.Codice, giocatoreModificato);
            }

            if (response.success)
            {
                await JS.InvokeVoidAsync("alert", "Operazione completata con successo!");

                
                giocatoreIns = new Giocatore();
                mostraModifica = false;
                Navigation.NavigateTo("/giocatori");
            }
            else
            {
                await JS.InvokeVoidAsync("alert", response.message);
            }
        }
    }
}

