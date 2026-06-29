using Microsoft.AspNetCore.Components;
using StoreShared.Interfaces;
using StoreShared.Models;

namespace StoreBlazor.Pages
{
    public partial class AreaDettaglioDipendenti
    {


        [Parameter]
        public int Id { get; set; }

        bool loading = false;
        public List<Dipendente>? dipendenti = new();

        [Inject]
        public IDipendentiService? DipendentiService { get; set; }

        public List<Area>? aree = new();

        Area? area { get; set; } = new();

        [Inject]
        public IAreeService? AreeService { get; set; }

        [Inject]
        public NavigationManager? Navigation {  get; set; }


        protected override async Task OnInitializedAsync()
        {
            loading = true;

            area = await AreeService!.GetArea(Id);


            dipendenti = await DipendentiService!.GetDipendenti();
            if (dipendenti != null)
            {
                dipendenti = dipendenti.Where(d => d.CodiceAreaAppl == area!.Codice).ToList();
            }
            
            loading = false;
        }

        void TornaIndietro(int id)
        {
            Navigation!.NavigateTo($"/aree/{id}");
        }




    }


}
