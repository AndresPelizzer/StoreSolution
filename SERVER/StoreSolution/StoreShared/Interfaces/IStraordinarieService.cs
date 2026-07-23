using StoreShared.Models.StoreDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreShared.Interfaces
{
    public interface IStraordinarieService
    {

        Task<List<Straordinaria>?> GetStraordinarie();

        Task<Straordinaria?> GetStraordinaria(int id);


        Task<Straordinaria> PostStraordinaria(Straordinaria straordinaria);


        Task DeleteStraordinaria(int id);

        Task<Straordinaria> PutStraordinaria( Straordinaria straordinaria, int id);
       
    }
}
