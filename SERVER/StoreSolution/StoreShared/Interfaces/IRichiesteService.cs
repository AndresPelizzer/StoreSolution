using StoreShared.Models;

namespace StoreShared.Interfaces
{
    public interface IRichiesteService
    {

        Task<List<Richiesta>?> GetRichieste();
        Task<Richiesta?> GetRichiesta(int id);

        Task<Richiesta?> AddRichiesta(Richiesta Richiesta);

        Task<Richiesta?> UpdateRichiesta(Richiesta Richiesta, int id);

        Task DeleteRichiesta(int id);


    }
}
