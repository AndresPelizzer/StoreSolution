using StoreShared.Models.StoreDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreShared.Interfaces
{
    public interface ISedeService
    {
      Task<List<Sede>?> GetSedi();
        Task<Sede?> GetSede(int id);
        Task DeleteSede(int id);
        Task<Sede> AddSede(Sede sede);
        Task<Sede> UpdateSede(int id, Sede sede);

    }
}
