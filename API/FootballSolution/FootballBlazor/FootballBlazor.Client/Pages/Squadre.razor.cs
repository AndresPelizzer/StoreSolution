namespace FootballBlazor.Client.Pages;

using FootballBlazor.Client.Models;
using FootballBlazor.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

public partial class Squadre
{
    [Inject]
    public HttpClient Http { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

    [Inject]
    public IJSRuntime JS { get; set; } = default!;


    private List<Shared.Models.Squadre> squadre = new();
    //List<Giocatori> giocatori = new List<Giocatori>();


    bool loading = false;



    protected override async Task OnInitializedAsync()
    {

    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            loading=true;
            StateHasChanged();
            var result = await Http.GetFromJsonAsync<List<Shared.Models.Squadre>>("api/squadre");
            if (result != null)
            {
                squadre = result;
            }


            //var resultGiocatori = await Http.GetFromJsonAsync<List<Giocatori>>("api/giocatori");
            //if (resultGiocatori != null)
            //{
            //    giocatori = resultGiocatori;
            //}


            loading = false;
            StateHasChanged();
        }

    }
    



    //public int ContaGiocatori(int idSquadra)
    //{
    //    return giocatori.Count(g => g.Idsquadra == idSquadra);
    //}

    public async Task EliminaSquadra(int id)
    {

        bool conferma = await JS.InvokeAsync<bool>(
            "confirm", "Sei sicuro di voler eliminare questa squadra?");

        if (!conferma)
        {
            return;
        }

        var risposta = await Http.DeleteAsync($"api/squadre/{id}");

        if (risposta.IsSuccessStatusCode)
        {
            squadre.RemoveAll(s => s.Idsquadra == id);
        }
    }



    public void VaiAllaSquadra(int id)
    {
        Navigation.NavigateTo($"/squadra/{id}");
    }

    public void VaiACreaSquadra()
    {

        Navigation.NavigateTo("/squadra/nuova");
    }

}