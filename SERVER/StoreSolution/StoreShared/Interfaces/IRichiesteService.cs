using StoreShared.Models.StoreDb;
using Microsoft.AspNetCore.Components.Forms;

namespace StoreShared.Interfaces
{
    public interface IRichiesteService
    {

        Task<List<Richiesta>?> GetRichieste();
        Task<Richiesta?> GetRichiesta(int id);

        Task<Richiesta?> AddRichiesta(Richiesta Richiesta);

        Task<Richiesta?> UpdateRichiesta(Richiesta Richiesta, int id);

        Task DeleteRichiesta(int id);

        Task UploadAllegato(int id, IBrowserFile file);


    }
}
