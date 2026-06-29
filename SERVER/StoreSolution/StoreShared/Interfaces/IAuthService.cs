using StoreShared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreShared.Interfaces
{
    public interface IAuthService
    {

        Task<LoginResponse?> Login(Credenziali credenziali);
        

    }
}
