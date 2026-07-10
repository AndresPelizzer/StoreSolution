using StoreShared.Models;
using StoreShared.Models.StoreDb;

namespace StoreShared.Interfaces
{
    public interface IClientiService
    {

        // Nel file IClientiService.cs
        Task<List<Cliente>?> GetClienti(int pageNumber = 1, int pageSize = 10);
        Task<Cliente?> GetCliente(int id);

        Task<Cliente?> AddCliente(Cliente Cliente);

        Task<Cliente?> UpdateCliente(Cliente Cliente, int id);

        Task DeleteCliente(int id);
        Task<ImportResult?> ImportClienti(Stream fileStream, string fileName);

        Task<ImportResult?> ImportClientiCsv(Stream fileStream, string fileName);
    }
}
