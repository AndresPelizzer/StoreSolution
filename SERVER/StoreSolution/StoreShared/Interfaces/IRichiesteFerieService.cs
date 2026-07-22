using StoreShared.Models.StoreDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreShared.Interfaces
{
    public interface IRichiesteFerieService
    {

        Task<List<RichiestaFerie>> GetFerie();
        Task<RichiestaFerie> GetFeria(int id);
        Task<RichiestaFerie> AddFeria(RichiestaFerie feria);

        Task DeleteFeria(int id);

        Task<RichiestaFerie?> UpdateFeria(int id, RichiestaFerie feria);


        Task<List<RichiestaFerie>> GetFerieDipendente(int id);
        Task<bool> AggiornaStato(int id, string stato);

    }
}
