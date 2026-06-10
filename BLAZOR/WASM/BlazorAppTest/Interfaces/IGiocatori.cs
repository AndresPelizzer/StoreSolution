using BlazorAppTest.Models;

namespace BlazorAppTest.Interfaces;

public interface IGiocatori
{
    public Task<List<Giocatore>> GetGiocatori();

    public Task<Giocatore> GetGiocatore(string codice);



    public Task<bool> AddGiocatore(Giocatore item);
    public Task<bool> UpdateGiocatore(string id, Giocatore newValues);
    public Task<bool> DeleteGiocatore(string id);
}
