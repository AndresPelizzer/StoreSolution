using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StoreBlazor.Services;
using StoreShared.Interfaces;
using StoreShared.Models.StoreDb;

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


        [Inject]
        IJSRuntime? JS { get; set; }

        [Inject]
        IUtentiService? UtentiService { get; set; }

        Area? area = new();

        
        public List<Utente>? utenti = new List<Utente>();
        Utente? utente = new();

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            if (Id != 0)
            {
                RichiestaModificata = await RichiesteService!.GetRichiesta(Id);
                clienti = await ClientiService!.GetClienti();
                aree = await AreeService!.GetAree();
                dipendenti = await DipendentiService!.GetDipendenti();
                utente = await UtentiService!.GetUtente(AuthState.CodiceUtente ?? 0);
                var dipendente = await DipendentiService!.GetDipendente(utente!.CodiceDipendente ?? 0);
                area = await AreeService!.GetArea(dipendente!.CodiceAreaAppl ?? 0);

            }
            else
            {
                NuovaRichiesta = new Richiesta();  
                var oraAttuale = DateTime.Now;
                NuovaRichiesta!.DataRichiesta = new DateTime(
                    oraAttuale.Year,
                    oraAttuale.Month,
                    oraAttuale.Day,
                    oraAttuale.Hour,
                    oraAttuale.Minute,
                    0, 0
                );

                clienti = await ClientiService!.GetClienti();
                aree = await AreeService!.GetAree();
                dipendenti = await DipendentiService!.GetDipendenti();
                var dipendente = await DipendentiService!.GetDipendente(utente!.CodiceDipendente ?? 0);
                area = await AreeService!.GetArea(dipendente!.CodiceAreaAppl ?? 0);
            }
            loading = false;
        }
        public async Task<object?> salvaRichiesta(Richiesta richiesta)
        {
            if (richiesta.CodiceCliente == null)
            {
                await JS!.InvokeVoidAsync("alert", "Attenzione:devi selezionare obbligatoriamente un cliente!!");
                return null;
            }
            var risultato = await RichiesteService!.AddRichiesta(richiesta) ?? new();
            if (risultato != null)
            {


                richieste = await RichiesteService.GetRichieste();
                Navigation!.NavigateTo("/richieste");

                return null;
            }
            else
            {
                await JS!.InvokeVoidAsync("alert", "Errore durante salvataggio lato server");
                return null;
            }
        }

        public async Task modificaRichiesta(Richiesta richiesta, int id)
        {
            await RichiesteService!.UpdateRichiesta(richiesta, id);

            richieste = await RichiesteService.GetRichieste();
            Navigation!.NavigateTo("/richieste");
        }
    }
}


