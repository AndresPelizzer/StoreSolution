using Microsoft.AspNetCore.Components;
using StoreBlazor.Services;
using StoreShared.Interfaces;
using StoreShared.Models;

namespace StoreBlazor.Pages
{
    public partial class ClientiRichiesteDipendenti
    {
        public Richiesta? richiesta = new();
        [Parameter]
        public int Codice { get; set; }
        [Inject]

        public IDipendentiService? DipendentiService { get; set; }

        [Inject]
        public IRichiesteService? RichiesteService { get; set; }
        public List <Richiesta> richieste = new List <Richiesta> ();

        public List<Dipendente> dipendenti = new();
        protected override async Task OnInitializedAsync()
        {

             richiesta = await RichiesteService!.GetRichiesta(Codice);



        }
    }
}
