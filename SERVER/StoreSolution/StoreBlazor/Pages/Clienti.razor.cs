using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StoreBlazor.Services;
using StoreShared.Interfaces;
using StoreShared.Models;
using System.Net.Http.Json;

namespace StoreBlazor.Pages
{
    public partial class Clienti
    {
        [Inject]
        public IClientiService? ClientiService { get; set; }

        [Inject]
        public IRichiesteService? RichiesteService { get; set; }
        [Inject]
        public IDipendentiService? DipendentiService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        [Inject]
        public IJSRuntime? JS {  get; set; }





        bool loading=false;
        bool showModal = false;
        bool showModalmod = false;
        bool showModaldett = false;
        bool showModaldipend = false;

        Cliente nuovoCliente = new Cliente();
        Cliente ClienteModificato= new Cliente( );

        public List<Cliente> clienti = new();

        public List<Richiesta> richieste = new();

        public List<Dipendente> dipendenti = new();



        protected override async Task OnInitializedAsync()
        {

            loading = true;
            clienti = await ClientiService!.GetClienti() ?? new();
            if (clienti != null)
            {
                loading = false;
            } 
        }



       public async Task salvaCliente(Cliente cliente)
        {

           
            var clientesalvato =await ClientiService!.AddCliente(cliente);

           if (clientesalvato != null) {
                clienti.Add(clientesalvato);
            }
            showModal = false;
            nuovoCliente = new();
            
        }

        void apriModifica(Cliente cliente)
        {
            ClienteModificato = new Cliente
            {
                Codice = cliente.Codice,
                Nome = cliente.Nome,
                Cognome = cliente.Cognome,
                Email = cliente.Email,
                Settore = cliente.Settore
            };
            showModalmod = true;
            showModaldett = false;
        }

        public async Task modificaCliente(Cliente cliente, int id)
        {
            var clientemodifica = await ClientiService!.UpdateCliente(cliente, id);

            
            clienti = await ClientiService!.GetClienti() ?? new();
            showModalmod = false;

        }



        public async Task elimina(int id)
        {

            var conferma = await JS!.InvokeAsync<bool>("confirm", "Sei sicuro di voler eliminare questo cliente?");
            if (conferma)
            {
                await ClientiService!.DeleteCliente(id);

                clienti.RemoveAll(c => c.Codice == id);
            }
            showModalmod = false;
        }

        public async Task apriDettaglio(Cliente cliente)
        {

            showModalmod = false;
            showModaldett = true;

            var tutteRichieste = await RichiesteService!.GetRichieste();
            richieste = tutteRichieste!.Where(r => r.CodiceCliente == cliente.Codice).ToList();


        }


        bool ascendente = true;
        string colonnaOrdinamento = "Nome";
        

        void Ordina(string colonna)
        {
            if (colonnaOrdinamento == colonna)
                ascendente = !ascendente;
            else
                colonnaOrdinamento = colonna;
        }



        public IEnumerable<Cliente> ClientiFiltrati => clienti
            .Where(c => string.IsNullOrEmpty(filtro) ||
                        c.Nome!.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                        c.Cognome!.Contains(filtro, StringComparison.OrdinalIgnoreCase))
             .OrderBy(c => colonnaOrdinamento switch
             {
                 "Nome" => c.Nome,
                 "Cognome" => c.Cognome,
                 "Email" => c.Email,
                 "Settore" => c.Settore,
                 _ => c.Nome
             });
            

   

        string filtro = "";





        public async Task apriDettDipend(Richiesta richiesta)
        {
            showModalmod = false;
            showModaldett = false;
            showModaldipend = true;

            var tuttiDipendenti = await DipendentiService!.GetDipendenti();

            dipendenti = tuttiDipendenti!.Where(d=> d.Codice == richiesta.CodiceDipendente).ToList();
        }


        public void VaiADettaglioCliente(int id)
        {
            Navigation.NavigateTo($"/clienti/{id}");
        }

    }

    }
        

       






