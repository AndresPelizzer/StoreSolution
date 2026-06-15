using FootballBlazor.Shared.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace FootballBlazor.Client.Pages;

public partial class SquadraDettaglio
{

    [Inject]
    public HttpClient Http { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    [Inject]
    public IJSRuntime JS { get; set; } = default!;

    Shared.Models.Squadre? squadra;

    //List<Giocatori> giocatori = new List<Giocatori>();

    protected override async Task OnInitializedAsync()
    {
        squadra = await Http.GetFromJsonAsync<Shared.Models.Squadre>($"api/squadre/{Id}");
        //var tuttiGiocatori = await Http.GetFromJsonAsync<List<Giocatori>>("api/giocatori");
        //if (tuttiGiocatori != null)
        //{
        //    giocatori = tuttiGiocatori.Where(g => g.Idsquadra == Id).ToList();
        //}
        //if(squadra != null) { 
        //giocatori = squadra?.Giocatori.ToList()!;
        //}
        

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


        squadra.NumeroGiocatoriInRosa = squadra.Giocatori.Count;

        bool esito = await AggiornaSquadra(Id, squadra);

        await JS.InvokeVoidAsync("alert",
            esito ? "Squadre aggiornata con successo!" : "Errore durante l'aggiornamento.");
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
