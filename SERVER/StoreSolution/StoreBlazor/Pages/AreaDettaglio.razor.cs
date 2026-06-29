using Microsoft.AspNetCore.Components;
using StoreBlazor.Services;
using StoreShared.Interfaces;
using StoreShared.Models;

namespace StoreBlazor.Pages
{
    public partial class AreaDettaglio
    {

        [Parameter]
        public int Id { get; set; }

        [Inject]
        NavigationManager? Navigation {  get; set; }

        [Inject]
        public IAreeService? AreeService { get; set; }

        Area NuovaArea { get; set; }=new Area();
        Area? AreaModificata { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (Id != 0)
            {
                AreaModificata = await AreeService!.GetArea(Id);
            }
        }


        public async Task salvaArea(Area area)
        {
            var areasalvata = await AreeService!.AddArea(area);

            if (areasalvata != null)
            {
                Navigation!.NavigateTo("/aree");
            }
        }
        public async Task modificaArea(Area area)
        {
            await AreeService!.UpdateArea(area, area.Codice ?? 0);
            Navigation!.NavigateTo("/aree");
        }

        void VisualizzaDipendentiArea(int id) {

            Navigation!.NavigateTo($"/area/{id}/dipendenti");
        }
    }
}

