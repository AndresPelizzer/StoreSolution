using BlazorAppTest.Interfaces;
using BlazorAppTest.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTest.Pages
{
    public partial class PageGiocatore
    {
        [Parameter] public string codice { get; set; } = string.Empty;

        
        [Inject] IGiocatori gService { get; set; } = null!;

        
        public Giocatore giocatore { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await Task.Delay(500);
            
            giocatore = await gService.GetGiocatore(codice);
        }
    }
}
