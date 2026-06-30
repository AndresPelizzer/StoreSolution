using Microsoft.AspNetCore.Components;
using StoreShared.Interfaces;

namespace StoreBlazor.Pages
{
    public partial class AdminHome
    {
        [Inject]
        public IClientiService? ClientiService { get; set; }

        [Inject]
        public IDipendentiService? DipendentiService { get; set; }

        [Inject]
        public IRichiesteService? RichiesteService { get; set; }

        [Inject]
        public IAreeService? AreeService { get; set; }

        bool loading = true;

        int totaleClienti = 0;
        int totaleDipendenti = 0;
        int totaleAree = 0;
        int richiesteAperte = 0;

        protected override async Task OnInitializedAsync()
        {
            var clienti = await ClientiService!.GetClienti();
            var dipendenti = await DipendentiService!.GetDipendenti();
            var aree = await AreeService!.GetAree();
            var richieste = await RichiesteService!.GetRichieste();

            totaleClienti = clienti?.Count ?? 0;
            totaleDipendenti = dipendenti?.Count ?? 0;
            totaleAree = aree?.Count ?? 0;
            richiesteAperte = richieste?.Count(r => r.Stato == "Aperta") ?? 0;

            loading = false;
        }
    }
}