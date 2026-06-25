using Microsoft.AspNetCore.Components;
using StoreBlazor.Services;
using StoreShared.Interfaces;
using StoreShared.Models;

namespace StoreBlazor.Pages
{
    public partial class ClienteRichieste
    {

        [Parameter]
        public int Id { get; set; }
        public List<Richiesta> richieste = new();


        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        [Inject]
        public IRichiesteService? RichiesteService { get; set; }
        protected override async Task OnInitializedAsync()
        {



            var tutteRichieste = await RichiesteService!.GetRichieste();
            richieste = tutteRichieste!.Where(r => r.CodiceCliente == Id).ToList();


        }

        void VaiDipendenti(int codice)
        {
            Navigation.NavigateTo($"/richieste/{codice}/dipendenti");
        }
        private string GetStatoClass(string stato) => stato switch
        {
            "Aperta" => "bg-success",
            "In lavorazione" => "bg-warning text-dark",
            "Conclusa" => "bg-secondary",
            
            _ => "bg-light text-dark"
        };



    }
}
