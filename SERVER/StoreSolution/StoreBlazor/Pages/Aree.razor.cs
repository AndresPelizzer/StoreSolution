using Microsoft.AspNetCore.Components;
using StoreShared.Interfaces;
using StoreShared.Models;

namespace StoreBlazor.Pages
{
    public partial class Aree
    {

        bool loading = false;

        [Inject]
        NavigationManager? Navigation {  get; set; }

        [Inject]
        public IAreeService? AreeService { get; set; }

        public List<Area>? aree = new();


        protected override async Task OnInitializedAsync()
        {
            loading = true;
            aree = await AreeService!.GetAree();
            if (aree != null) {

                loading = false;
            }

        }

        public void VaiArea(int id)
        {
            Navigation!.NavigateTo($"aree/{id}");
        }


    }
}




