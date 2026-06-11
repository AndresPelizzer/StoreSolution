using BlazorAppTest.Models;

namespace BlazorAppTest.Interfaces
{
    public interface IGiocatori
    {
        public Task<List<Giocatore>> GetGiocatori();

        public Task<Giocatore> GetGiocatore(int codice);

        public Task<ApiResponse> AddGiocatore(Giocatore item);



        public Task<ApiResponse> UpdateGiocatore(int id, Giocatore newValues);

       
        public Task<bool> DeleteGiocatore(int id);
    }
}