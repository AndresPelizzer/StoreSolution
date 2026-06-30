using Microsoft.AspNetCore.Components;
using StoreShared.Interfaces;
using StoreShared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreBlazor.Pages
{
    public partial class Aree
    {
        private bool loading = false;

        [Inject] public NavigationManager? Navigation { get; set; }
        [Inject] public IAreeService? AreeService { get; set; }

        public List<Area>? aree = new();

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            try
            {
                aree = await AreeService!.GetAree() ?? new List<Area>();
            }
            finally
            {
                loading = false;
            }
        }

        public void VaiArea(int id)
        {
            Navigation!.NavigateTo($"aree/{id}");
        }

        public async Task elimina(int id)
        {
            await AreeService!.DeleteArea(id);
            aree!.RemoveAll(a => a.Codice == id);
        }


    }
}