using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StoreShared.Interfaces;
using StoreShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreBlazor.Pages
{
    public partial class Clienti
    {
        [Inject] public IClientiService? ClientiService { get; set; }
        [Inject] public IRichiesteService? RichiesteService { get; set; }
        [Inject] public IDipendentiService? DipendentiService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; } = default!;
        [Inject] public IJSRuntime? JS { get; set; }

        private bool loading = false;
        private bool showModal = false;
        private bool showModalmod = false;
        private bool showModaldett = false;
        private bool showModaldipend = false;

        private Cliente nuovoCliente = new Cliente();
        private Cliente ClienteModificato = new Cliente();

        public List<Cliente> clienti = new();
        public List<Richiesta> richieste = new();
        public List<Dipendente> dipendenti = new();

        private string filtro = "";
        private bool ascendente = true;
        private string colonnaOrdinamento = "Nome";

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            try
            {
                clienti = await ClientiService!.GetClienti() ?? new();
            }
            finally
            {
                loading = false;
            }
        }

        public async Task salvaCliente(Cliente cliente)
        {
            var clientesalvato = await ClientiService!.AddCliente(cliente);
            if (clientesalvato != null)
            {
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
            await ClientiService!.UpdateCliente(cliente, id);
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

        void Ordina(string colonna)
        {
            if (colonnaOrdinamento == colonna)
                ascendente = !ascendente;
            else
            {
                colonnaOrdinamento = colonna;
                ascendente = true;
            }
        }

       
        public IEnumerable<Cliente> ClientiFiltrati
        {
            get
            {
                var query = clienti.Where(c => string.IsNullOrEmpty(filtro) ||
                                               (c.Nome != null && c.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase)) ||
                                               (c.Cognome != null && c.Cognome.Contains(filtro, StringComparison.OrdinalIgnoreCase)));

                return ascendente
                    ? query.OrderBy(c => OttieniValoreProprieta(c, colonnaOrdinamento))
                    : query.OrderByDescending(c => OttieniValoreProprieta(c, colonnaOrdinamento));
            }
        }

        private string OttieniValoreProprieta(Cliente c, string colonna) => colonna switch
        {
            "Nome" => c.Nome ?? "",
            "Cognome" => c.Cognome ?? "",
            "Email" => c.Email ?? "",
            "Settore" => c.Settore ?? "",
            _ => c.Nome ?? ""
        };

        public async Task apriDettDipend(Richiesta richiesta)
        {
            showModalmod = false;
            showModaldett = false;
            showModaldipend = true;

            var tuttiDipendenti = await DipendentiService!.GetDipendenti();
            dipendenti = tuttiDipendenti!.Where(d => d.Codice == richiesta.CodiceDipendente).ToList();
        }

        
        public void ApriNuovoCliente()
        {
            nuovoCliente = new Cliente();
            showModal = true;
            Navigation.NavigateTo("/clienti/0");
        }

        public void VaiADettaglioCliente(int id)
        {
            Navigation.NavigateTo($"/clienti/{id}");
        }
    }
}