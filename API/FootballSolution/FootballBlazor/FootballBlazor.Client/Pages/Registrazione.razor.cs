using Azure;
using FootballBlazor.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace FootballBlazor.Client.Pages
{
    public partial class Registrazione
    {
        [Inject]
        public HttpClient Http { get; set; } = default!;
        [Inject]
        public NavigationManager Navigation { get; set; } = default!;
        private Utenti user { get; set; } = new Utenti();
        private string ConfirmPassword = string.Empty;
        private string message = string.Empty;
        bool successo = false;


        public async Task iscrizione()
        {
            if (string.IsNullOrWhiteSpace(user.Nome))
            {
                this.message = "Il nome è obbligatorio!";
                return;
            }

            else if (!char.IsUpper(user.Nome[0]))
            {
                this.message = "Il nome deve iniziare con una lettera maiuscola!";
                return;
            }

            else if (string.IsNullOrWhiteSpace(user.Username))
            {
                this.message = "L'username è obbligatorio!";
                return;
            }

            else if (user.Username.Length < 6)
            {
                this.message = "La lunghezza dell'username deve essere almeno di 6 caratteri!";
                return;
            }

            else if (string.IsNullOrWhiteSpace(user.Email))
            {
                this.message = "Email obbligatoria!";
                return;
            }

            else if (string.IsNullOrWhiteSpace(user.Ruolo))
            {
                this.message = "Seleziona un ruolo!";
                return;
            }

            else if (string.IsNullOrWhiteSpace(user.Password))
            {
                this.message = "La password è obbligatoria!";
                return;
            }

            else if (user.Password.Length < 6)
            {
                this.message = "La password deve essere almeno di 6 caratteri!";
                return;
            }

            else if(!user.Password.Any(c => "!@#$%^&*()_+-=[]{}|;:',.<>?/".Contains(c)))
            {
                this.message = "La password deve contenere almeno un carattere speciale!";
                return;
            }


            try
            {
                this.message = "";

                if (this.user.Password != ConfirmPassword)
                {

                    this.message = "Le password NON coincidono...";
                    return;
                }


                var http_response = await Http.PostAsJsonAsync("/api/Utenti/register", this.user);
                Result? response = await http_response.Content.ReadFromJsonAsync<Result>();





                //var httpResponse = await Http.PostAsJsonAsync("/api/Utenti/register", user);

                //var content = await httpResponse.Content.ReadAsStringAsync();

                //this.message =
                //    $"Status: {httpResponse.StatusCode}\n" +
                //    $"Reason: {httpResponse.ReasonPhrase}\n" +
                //    $"Content: {content}";




                if (response == null)
                {
                    this.message = "non ha funzionato...";
                    return;
                }
                else if (response.Success == true)
                {

                    successo = true;
                    this.message = "registrazione avvenuta con successo";
                    StateHasChanged();
                    await Task.Delay(1600);

                    Navigation.NavigateTo("/home");
                    return;
                }
                else
                {
                    this.message = response.Message;
                    return;
                }
            }
            catch (Exception ex) {

                this.message = ex.Message;

            }



           





        }
    }
}
