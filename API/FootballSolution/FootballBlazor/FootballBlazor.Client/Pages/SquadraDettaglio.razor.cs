using FootballBlazor.Shared.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Net.Http.Json;

namespace FootballBlazor.Client.Pages;

public partial class SquadraDettaglio
{

    [Inject]
    public HttpClient Http { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    [Inject]
    public DialogService MyDialogService { get; set; } = default!;


    Shared.Models.Squadre? squadra;

    //List<Giocatori> giocatori = new List<Giocatori>();

    bool isNew = false;

    protected override async Task OnInitializedAsync()
    {
        if (Id == 0)
        {
            isNew = true;
            squadra = new Shared.Models.Squadre();
        }
        else
        {
            squadra = await Http.GetFromJsonAsync<Shared.Models.Squadre>($"api/squadre/{Id}");
        }

    }

    public async Task<bool> AggiornaSquadra(int id, Shared.Models.Squadre nuovivalori)
    {
        try
        {
            var risposta = await Http.PutAsJsonAsync($"api/squadre/{id}", nuovivalori);
            return risposta.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task SalvaModifica()
    {
        if (squadra == null) return;

        if (Id == 0)
        {
            
            var risposta = await Http.PostAsJsonAsync("api/squadre", squadra);
            if (risposta.IsSuccessStatusCode)
            {
                await MyDialogService.Alert("Squadra creata con successo!");
                Navigation.NavigateTo("/squadre");
            }
            else
            {
                await MyDialogService.Alert("Errore durante la creazione...");
            }
        }
        else
        {
            
            squadra.NumeroGiocatoriInRosa = squadra.Giocatori.Count;
            bool esito = await AggiornaSquadra(Id, squadra);

            if (esito)
                await MyDialogService.Alert("Squadra aggiornata con successo!");
            else
                await MyDialogService.Alert("Errore durante l'aggiornamento...");
        }
    }
    [Inject]
    public NavigationManager Navigation { get; set; } = default!;


    public void TornaAllaLista()
    {

        Navigation.NavigateTo("/squadre");
    }


    public void VaiACreaGiocatore()
    {
        Navigation.NavigateTo($"/giocatore/nuovo/{Id}");
    }

}
