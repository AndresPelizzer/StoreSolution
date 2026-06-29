using Microsoft.AspNetCore.Components;
using StoreShared.Interfaces;
using StoreShared.Models;

namespace StoreBlazor.Pages
{
    public partial class Richieste
    {
        [Inject]
        public NavigationManager? Navigation {  get; set; }

        [Inject]
        IRichiesteService? RichiesteService { get; set; }


        public List<Richiesta>? richieste = new List<Richiesta>();


        bool loading = false;
        protected override async Task OnInitializedAsync(){

            loading = true;
            richieste = await RichiesteService!.GetRichieste();
            loading = false;

    }


        public void VaiADettaglioRichieste(int id)
        {


            Navigation!.NavigateTo($"richiesta/{id}");

        }

        public async Task elimina(int id)
        {
            await RichiesteService!.DeleteRichiesta(id);
            richieste!.RemoveAll(r => r.Codice == id);
        }
}
}
