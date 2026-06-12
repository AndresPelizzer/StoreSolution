using FootballBlazor.Client.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace FootballBlazor.Client.Pages;

using FootballBlazor.Shared.Models;


public partial class GiocatoreDettaglio
{
    [Inject]
    public HttpClient Http { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    Giocatori? giocatore;

    protected override async Task OnInitializedAsync()
    {
        giocatore = await Http.GetFromJsonAsync<Giocatori>($"api/giocatori/{Id}");
    }

    public async Task<bool> AggiornaGiocatore(int id, Giocatori nuovivalori)
    {
        try
        {
            var risposta = await Http.PutAsJsonAsync($"api/giocatori/{id}", nuovivalori);
            return risposta.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task SalvaModifica()
    {
        if (giocatore != null)
        {
            bool esito = await AggiornaGiocatore(Id, giocatore);

            if (esito)
                Console.WriteLine("Giocatori aggiornato con successo!");
            else
                Console.WriteLine("Errore durante l'aggiornamento.");
        }
    }


    public void TornaAllaSquadra()
    {
        if (giocatore?.Idsquadra != null)
        {
            Navigation.NavigateTo($"/squadra/{giocatore.Idsquadra}");
        }
        else
        {
            Navigation.NavigateTo("/squadre");
        }
    }

   
}