using Microsoft.AspNetCore.Components;
using StoreShared.Interfaces;
using StoreShared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreBlazor.Pages
{
    public partial class Richieste
    {
        [Inject] public NavigationManager? Navigation { get; set; }
        [Inject] IRichiesteService? RichiesteService { get; set; }

        public List<Richiesta>? richieste = new List<Richiesta>();
        private bool loading = false;

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            try
            {
                richieste = await RichiesteService!.GetRichieste() ?? new List<Richiesta>();
            }
            finally
            {
                loading = false;
            }
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

       
        private string GetStatoBadgeClass(string? stato)
        {
            if (string.IsNullOrEmpty(stato)) return "bg-secondary-subtle text-secondary";

            return stato.ToLower() switch
            {
                "nuova" or "aperta" => "bg-info-subtle text-info border border-info-subtle",
                "in lavorazione" or "assegnata" => "bg-warning-subtle text-warning border border-warning-subtle",
                "chiusa" or "completata" => "bg-success-subtle text-success border border-success-subtle",
                "rifiutata" or "annullata" => "bg-danger-subtle text-danger border border-danger-subtle",
                _ => "bg-light text-dark border"
            };
        }
    }
}