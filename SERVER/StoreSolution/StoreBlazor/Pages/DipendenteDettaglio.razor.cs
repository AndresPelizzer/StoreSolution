using Microsoft.AspNetCore.Components;
using StoreShared.Interfaces;
using StoreShared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreBlazor.Pages
{
    public partial class DipendenteDettaglio
    {
        [Parameter]
        public int Id { get; set; }

        Dipendente? NuovoDipendente { get; set; } = new();
        Dipendente DipendenteModificato { get; set; } = new Dipendente();

       
        private List<Area> ListaAree { get; set; } = new();

        [Inject]
        public IDipendentiService? DipendentiService { get; set; }

      
        [Inject]
        public IAreeService? AreeService { get; set; }

        [Inject]
        public NavigationManager? Navigation { get; set; }

        bool loading = true;

        protected override async Task OnInitializedAsync()
        {
            
            if (AreeService != null)
            {
                var aree = await AreeService.GetAree(); 
                ListaAree = aree?.ToList() ?? new List<Area>();
            }

            
            if (Id != 0)
            {
                DipendenteModificato = await DipendentiService!.GetDipendente(Id) ?? new();
            }

            loading = false;
        }

        async Task salvaDipendente(Dipendente dipendente)
        {
            var salvato = await DipendentiService!.AddDipendente(dipendente);
            if (salvato != null)
                Navigation!.NavigateTo("/dipendenti");
        }

        async Task modificaDipendente(Dipendente dipendente, int id)
        {
            await DipendentiService!.UpdateDipendente(dipendente, id);
            Navigation!.NavigateTo("/dipendenti");
        }
    }
}

