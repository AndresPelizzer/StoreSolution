using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StoreShared.Interfaces;
using StoreShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreBlazor.Pages
{
    public partial class Dipendenti
    {
        [Inject] public IJSRuntime? JS { get; set; }
        [Inject] public IDipendentiService? DipendentiService { get; set; }
        [Inject] public NavigationManager? Navigation { get; set; }

        private bool loading = false;
        public List<Dipendente>? dipendenti = new List<Dipendente>();

        private string filtro = "";
        private bool ascendente = true;
        private string colonnaOrdinamento = "Nome";

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            try
            {
                dipendenti = await DipendentiService!.GetDipendenti() ?? new();
            }
            finally
            {
                loading = false;
            }
        }

        private void Ordina(string colonna)
        {
            if (colonnaOrdinamento == colonna)
            {
                ascendente = !ascendente;
            }
            else
            {
                colonnaOrdinamento = colonna;
                ascendente = true;
            }
        }

        public IEnumerable<Dipendente> DipendentiFiltrati
        {
            get
            {
                if (dipendenti == null) return Enumerable.Empty<Dipendente>();

                var query = dipendenti.Where(d => string.IsNullOrEmpty(filtro) ||
                                                 (d.Nome != null && d.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase)) ||
                                                 (d.Cognome != null && d.Cognome.Contains(filtro, StringComparison.OrdinalIgnoreCase)));

                return ascendente
                    ? query.OrderBy(d => OttieniValoreProprieta(d, colonnaOrdinamento))
                    : query.OrderByDescending(d => OttieniValoreProprieta(d, colonnaOrdinamento));
            }
        }

        private string OttieniValoreProprieta(Dipendente d, string colonna) => colonna switch
        {
            "Nome" => d.Nome ?? "",
            "Cognome" => d.Cognome ?? "",
            "Email" => d.Email ?? "",
            "Qualifica" => d.Qualifica ?? "",
            "Note" => d.Note ?? "",
            _ => d.Nome ?? ""
        };

        public async Task elimina(int id)
        {
            var conferma = await JS!.InvokeAsync<bool>("confirm", "Sei sicuro di voler eliminare questo dipendente?");
            if (conferma)
            {
                await DipendentiService!.DeleteDipendente(id);
                dipendenti!.RemoveAll(d => d.Codice == id);
            }
        }

        private void VaiADettaglioDipendente(int id)
        {
            Navigation!.NavigateTo($"/dipendenti/{id}");
        }
    }
}