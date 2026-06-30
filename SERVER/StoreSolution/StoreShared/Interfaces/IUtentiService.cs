using StoreShared.Models;

namespace StoreShared.Interfaces
{
    public interface IUtentiService
    {

        Task<List<Utente>?> GetUtenti();
        Task<Utente?> GetUtente(int id);

        Task<Utente?> AddUtente(Utente Utente);

        Task<Utente?> UpdateUtente(Utente Utente, int id);

        Task DeleteUtente(int id);


    }
}

