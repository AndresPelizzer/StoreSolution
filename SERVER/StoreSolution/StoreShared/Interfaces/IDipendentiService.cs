using StoreShared.Models;

namespace StoreShared.Interfaces
{
    public interface IDipendentiService
    {

        Task<List<Dipendente>?> GetDipendenti();
        Task<Dipendente?> GetDipendente(int id);

        Task<Dipendente?> AddDipendente(Dipendente dipendente);

        Task<Dipendente?> UpdateDipendente(Dipendente dipendente, int id);

        Task DeleteDipendente(int id);


    }
}
