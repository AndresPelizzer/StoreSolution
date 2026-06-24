using Microsoft.AspNetCore.Components;
using StoreShared.Interfaces;
using StoreShared.Models;
using System.Net.Http.Json;

namespace StoreBlazor.Pages
{
    public partial class Clienti
    {
        [Inject]
        public IClientiService? ClientiService { get; set; }

        bool loading=false;
        bool showModal = false;
        bool showModalmod = false;
        bool showModaldett = false;

        Cliente nuovoCliente = new Cliente();
        Cliente ClienteModificato= new Cliente( );

        public List<Cliente> clienti = new();



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
        }

        public async Task modificaCliente(Cliente cliente, int id)
        {
            var clientemodifica = await ClientiService!.UpdateCliente(cliente, id);

            
            clienti = await ClientiService!.GetClienti() ?? new();
            showModalmod = false;

        }



        public async Task elimina(int id)
        {
            await ClientiService!.DeleteCliente(id);

            clienti.RemoveAll(c => c.Codice == id);
            showModalmod = false;
        }

        void apriDettaglio(Cliente cliente)
        {
            showModaldett = true;
        }









    }
        

        }
      

