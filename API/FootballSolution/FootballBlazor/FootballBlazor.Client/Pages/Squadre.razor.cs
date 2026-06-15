
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Net.Http.Json;

namespace FootballBlazor.Client.Pages;
public partial class Squadre
{
    [Inject]
    public HttpClient Http { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;


    [Inject]
    public DialogService MyDialogService { get; set; } = default!;


    private List<Shared.Models.Squadre> squadre = new();
    //List<Giocatori> giocatori = new List<Giocatori>();


    bool loading = false;
    string filter { get; set; } = "";




    bool mostratabella1 = true;

    bool mostratabella2 = true;


    protected override async Task OnInitializedAsync() { 
    
        
            //loading = true;
            //StateHasChanged();
            //var result = await Http.GetFromJsonAsync<List<Shared.Models.Squadre>>("api/squadre");
            //if (result != null)
            //{
            //    squadre = result;
            //}


            ////var resultGiocatori = await Http.GetFromJsonAsync<List<Giocatori>>("api/giocatori");
            ////if (resultGiocatori != null)
            ////{
            ////    giocatori = resultGiocatori;
            ////}


            //loading = false;
            //StateHasChanged();
        }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            loading = true;
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
        Console.WriteLine($"Il bottone è stato premuto per l'id: {id}");
        bool? conferma = await MyDialogService.Confirm(
            "Sei sicuro di voler eliminare questa squadra?",
            "Conferma Eliminazione",
            new ConfirmOptions() { OkButtonText = "Sì", CancelButtonText = "No" });

        if (conferma != true) return;

        var risposta = await Http.DeleteAsync($"api/squadre/{id}");

        if (risposta.IsSuccessStatusCode)
        {
           
            squadre.RemoveAll(s => s.Idsquadra == id);
            
            squadre = new List<Shared.Models.Squadre>(squadre);
            StateHasChanged();
        }
    }



    public void VaiAllaSquadra(int id)
    {
        Navigation.NavigateTo($"/squadra/{id}");
    }

    public void VaiACreaSquadra()
    {

        Navigation.NavigateTo("/squadra/0");
    }

    int? squadraAperta = null;

    public void ToggleDettaglio(int id)
    {
        squadraAperta = (squadraAperta == id) ? null : id;
    }



    //private void Cerca()
    //{
    //    squadre = squadre.Where(s => s.NomeSquadra.Contains(this.filter)).ToList();
    //}

    //bool ordina = false;

    string ordina = "";


    private void Ordina()
    {
        if (ordina == "") { ordina = "ASC"; }
        else if (ordina == "ASC") { ordina = "DESC"; }
        else if (ordina == "DESC") { ordina = "ASC"; }
        




        if (ordina == "ASC")
        {
            squadre = squadre.OrderBy(s => s.NomeSquadra).ToList();
        } else if ( ordina == "DESC")
        {
            squadre = squadre.OrderByDescending(s => s.NomeSquadra).ToList();
        }

        //ordina = true;
    }
}

 
