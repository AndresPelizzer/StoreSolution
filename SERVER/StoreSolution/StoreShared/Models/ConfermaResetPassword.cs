using System;
using System.Collections.Generic;
using System.Text;

namespace StoreShared.Models
{
    public class ConfermaResetPassword
    {
        public string? Token { get; set; }
        public string? NuovaPassword { get; set; }

        public static implicit operator ConfermaResetPassword(HttpResponseMessage v)
        {
            throw new NotImplementedException();
        }
    }
}
