using BCrypt.Net;
using Microsoft.AspNetCore.Components;
using StoreBlazor.Services;
using StoreShared.Interfaces;
using StoreShared.Models.StoreDb;
using System.Runtime.CompilerServices;

namespace StoreBlazor.Pages
{
    public partial class ClienteDettaglio
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public HttpClient Http { get; set; } = default!;

       Cliente ClienteModificato= new Cliente();
        Cliente NuovoCliente= new Cliente();

        public List<Cliente> clienti = new();

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        [Inject]
        public IClientiService ClientiService { get; set; } = default!;
        public List<Richiesta> richieste = new();

        [Inject]
        public IRichiesteService RichiesteService { get; set; } = default!;


        Utente nuovoUtente = new Utente();


        public string errore = "";


        [Inject]
        public IUtentiService UtentiService { get; set; }= default!;


        //public async Task modifica(int id)
        //{
        //    clienti = await ClientiService!.GetClienti() ?? new();
        //    if (clienti != null)
        //    {
        //        var ClienteModificato = clienti.Where(c => c.Codice == id);
        //    }
           

            
        //}
        
        
        public async Task modificaCliente(Cliente cliente, int id)
        {
            var clientemodifica = await ClientiService!.UpdateCliente(cliente, id);


            clienti = await ClientiService!.GetClienti() ?? new();
            Navigation.NavigateTo("/clienti");


        }
        public async Task salvaCliente(Cliente cliente)
        {

            ValidaInput();
            if (!successo) return;


                clienti = await ClientiService!.GetClienti() ?? new();
                var clientesalvato = await ClientiService!.AddCliente(cliente);

                if (clientesalvato != null)
                {
                    clienti.Add(clientesalvato);

                    nuovoUtente.CodiceCliente = clientesalvato.Codice;
                    nuovoUtente.PasswordHash = BCrypt.Net.BCrypt.HashPassword(nuovoUtente.PasswordHash);
                    await UtentiService.AddUtente(nuovoUtente);

                    Navigation.NavigateTo("/clienti");

                }
         
        }


        public async Task apriDettaglio(Cliente cliente)
        {

            

            var tutteRichieste = await RichiesteService!.GetRichieste();
            richieste = tutteRichieste!.Where(r => r.CodiceCliente == cliente.Codice).ToList();


        }

        void VaiARichieste(int id)
        {
            Navigation.NavigateTo($"/clienti/{id}/richieste");
        }

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0)
            {
                ClienteModificato = await ClientiService!.GetCliente(Id) ?? new();
            }
            nuovoUtente.Ruolo = "cliente";
        }

        private bool successo = false;
        private void ValidaInput()
        {
            if (NuovoCliente.Nome?.Length < 6)
            {
                errore = "Inserisci un nome di almeno 6 caratteri";
            }
            else if (NuovoCliente.Cognome?.Length < 6)
            {
                errore = "Inserisci un cognome di almeno 6 caratteri";
            }
            else if (string.IsNullOrEmpty(NuovoCliente.Email) || !NuovoCliente.Email.Contains("@") || !NuovoCliente.Email.Contains("."))
            {
                errore = "Inserisci il formato corretto per l'email!!";
            }

            else
            {
                errore = "";
                successo = true;
            }
        }

    }



}
