using StoreShared.Models.StoreDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreShared.Interfaces
{
    public interface IAttDipService
    {
        Task<List<AttDip>> GetAttsDip();

        Task<AttDip> GetAttDip(int id);

        Task<AttDip?> AddAttDip(AttDip attDip);


        Task<AttDip?> UpdateAttDip(AttDip AttDip, int id);

        Task DeleteAttDip(int id);



        
    }
}
