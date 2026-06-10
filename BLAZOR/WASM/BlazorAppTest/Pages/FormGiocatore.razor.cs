using BlazorAppTest.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTest.Pages
{
    public partial class FormGiocatore
    {
        [Parameter]
        public Giocatore GiocatoreIns {  get; set; }= new Giocatore();

        [Parameter]
        public EventCallback OnSalva { get; set; }
    }
}
