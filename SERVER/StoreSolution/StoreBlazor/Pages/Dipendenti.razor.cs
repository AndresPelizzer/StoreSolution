
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StoreShared.Interfaces;
using StoreShared.Models;

namespace StoreBlazor.Pages
{
    public partial class Dipendenti
    {

        bool loading = false;

        public List<Dipendente>? dipendenti = new List<Dipendente>();
        [Inject]
        IJSRuntime? JS {  get; set; }

        [Inject]
        public IDipendentiService? DipendentiService { get; set; }

        [Inject]
        NavigationManager?  Navigation {  get; set; }

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            dipendenti = await DipendentiService!.GetDipendenti();
            loading = false;
        }

        string filtro = "";
        bool ascendente = true;
        string colonnaOrdinamento = "Nome";


        void Ordina(string colonna)
        {
            if (colonnaOrdinamento == colonna)
                ascendente = !ascendente;
            else
                colonnaOrdinamento = colonna;
        }
        public IEnumerable<Dipendente> DipendentiFiltrati => dipendenti!
            .Where(d => string.IsNullOrEmpty(filtro) ||
                        d.Nome!.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                        d.Cognome!.Contains(filtro, StringComparison.OrdinalIgnoreCase))
             .OrderBy(d => colonnaOrdinamento switch
             {
                 "Nome" => d.Nome!,
                 "Cognome" => d.Cognome!,
                 "Email" => d.Email!,
                 "Qualifica" => d.Qualifica!,
                 
                
                 "Note"=>d.Note!,

                 _ => d.Nome!
             });

        public async Task elimina(int id)
        {
            await DipendentiService!.DeleteDipendente(id);
            var conferma = await JS!.InvokeAsync<bool>("confirm", "Sei sicuro di voler eliminare questo dipendente?");
            if (conferma)
            {
                dipendenti!.RemoveAll(d => d.Codice == id);
            }
        }

        void VaiADettaglioDipendente(int id)
        {
            Navigation!.NavigateTo($"/dipendenti/{id}");
        }
    }
}
