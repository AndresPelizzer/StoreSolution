using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace FootballBlazor.Client.Pages
{
    public partial class Utenti
    {
        [Inject]
        public HttpClient Http { get; set; } = default!;




        private List<Shared.Models.Utenti> utenti = new();
        bool loading = true;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                loading = true;
                var result = await Http.GetFromJsonAsync<List<Shared.Models.Utenti>>("api/utenti");
                if (result != null)
                {
                    utenti = result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel caricamento utenti: {ex.Message}");
            }

            finally
            {
                loading = false;
            }

        }



        private async Task ScaricaCurriculum(Shared.Models.Utenti utente)
        {
            if (utente.Curriculum != null && utente.Curriculum.Length > 0)
            {
                byte[] fileBytes = utente.Curriculum;

                //var base64 = Convert.ToBase64String(utente.Curriculum);

                //await JS.InvokeVoidAsync(
                //    "downloadFileFromBytes",
                //    $"{utente.Nome}_CV.pdf",
                //    base64);


                string excelFileName = $"{utente.Nome}_CV.pdf";
                // Call JavaScript to download the file
                await JS.InvokeVoidAsync("downloadFile", excelFileName, fileBytes);



            }
        }
    }
}
                
        
    
