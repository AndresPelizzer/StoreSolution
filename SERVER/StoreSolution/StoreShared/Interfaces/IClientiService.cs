using StoreShared.Models;

namespace StoreShared.Interfaces
{
    public interface IClientiService
    {

        Task<List<Cliente>?> GetClienti();
        Task<Cliente?> GetCliente(int id);

        Task<Cliente?> AddCliente(Cliente Cliente);

        Task<Cliente?> UpdateCliente(Cliente Cliente, int id);

        Task DeleteCliente(int id);
        Task<ImportResult?> ImportClienti(Stream fileStream, string fileName);

        Task<ImportResult?> ImportClientiCsv(Stream fileStream, string fileName);
    }
}
