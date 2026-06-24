using StoreShared.Models;

namespace StoreShared.Interfaces
{
    public interface IAreeService
    {

        Task<List<Area>?> GetAree();
        Task<Area?> GetArea(int id);

        Task<Area?> AddArea(Area Area);

        Task<Area?> UpdateArea(Area Area, int id);

        Task DeleteArea(int id);


    }
}
