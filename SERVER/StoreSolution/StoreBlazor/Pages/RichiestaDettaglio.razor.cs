using Microsoft.AspNetCore.Components;
using StoreBlazor.Services;
using StoreShared.Interfaces;
using StoreShared.Models;

namespace StoreBlazor.Pages
{
    public partial class RichiestaDettaglio
    {

        [Parameter]
        public int Id { get; set; }

        [Inject]
        NavigationManager? Navigation {  get; set; }
        bool loading = false;

        [Inject]
        IRichiesteService? RichiesteService { get; set; }

        public List<Richiesta>? richieste = new List<Richiesta>();

        public List<Cliente>? clienti= new List<Cliente>();

        [Inject]
        IClientiService? ClientiService { get; set; }


        [Inject]
        IAreeService? AreeService { get; set; }

        public List<Area>? aree = new();


        [Inject]

        IDipendentiService? DipendentiService { get; set; }
        public List<Dipendente>? dipendenti= new List<Dipendente>();

        Richiesta? NuovaRichiesta { get; set; }

        Richiesta? RichiestaModificata { get; set; }

        

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            if (Id != 0)
            {
                RichiestaModificata = await RichiesteService!.GetRichiesta(Id);
                clienti = await ClientiService!.GetClienti();
                aree = await AreeService!.GetAree();
                dipendenti = await DipendentiService!.GetDipendenti();
            }
            else
            {
                NuovaRichiesta = new Richiesta();
                clienti = await ClientiService!.GetClienti();
                aree = await AreeService!.GetAree();
                dipendenti = await DipendentiService!.GetDipendenti();
                
            }
            loading = false;
            
        }
        public async Task salvaRichiesta(Richiesta richiesta)
        {
            richiesta = await RichiesteService!.AddRichiesta(richiesta) ?? new();

            richieste = await RichiesteService.GetRichieste();
            Navigation!.NavigateTo("/richieste");
        }

        public async Task modificaRichiesta(Richiesta richiesta, int id)
        {
            await RichiesteService!.UpdateRichiesta(richiesta, id);

            richieste = await RichiesteService.GetRichieste();
            Navigation!.NavigateTo("/richieste");
        }
    }
}


