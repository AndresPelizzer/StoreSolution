using BCrypt.Net;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StoreBlazor.Services;
using StoreShared.Interfaces;
using StoreShared.Models.StoreDb;
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

        [Inject]
        public IUtentiService? UtentiService {  get; set; }

       
        private List<Area> ListaAree { get; set; } = new();

        [Inject]
        public IDipendentiService? DipendentiService { get; set; }

      
        [Inject]
        public IAreeService? AreeService { get; set; }

        [Inject]
        public NavigationManager? Navigation { get; set; }

        bool loading = true;

        Utente? Utente = new();

        [Inject]
        IJSRuntime? JS { get; set; }

        public List<Dipendente>?dipendenti = new();
        

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

            Utente!.Ruolo = "dipendente";




         
            
        }

        async Task salvaDipendente(Dipendente dipendente)
        {
            if (dipendente.CapoArea)
            {
                dipendenti = await DipendentiService!.GetDipendenti();
                bool presente = dipendenti!.Any(d => d.CapoArea == dipendente.CapoArea && d.CodiceAreaAppl == dipendente.CodiceAreaAppl);
                if (presente)
                {
                    await JS!.InvokeVoidAsync("alert","Non puoi inserire piu di un capo d'area all'interno della stessa area!!");
                }
            }
           
            var salvato = await DipendentiService!.AddDipendente(dipendente);
            if (salvato != null)
            {



                Utente!.CodiceDipendente = salvato!.Codice;
                //Utente!.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Utente.PasswordHash);

                await UtentiService!.AddUtente(Utente!);


                Navigation!.NavigateTo("/dipendenti");
            }
            

        }

        async Task modificaDipendente(Dipendente dipendente, int id)
        {
            var risultato=await DipendentiService!.UpdateDipendente(dipendente, id);
            if (risultato == null)
            {
                await JS!.InvokeVoidAsync("alert", "Troppi capi d'area");
            }
            Navigation!.NavigateTo("/dipendenti");
        }
    }
}

