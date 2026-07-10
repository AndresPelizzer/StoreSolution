using System;
using System.Collections.Generic;
using System.Text;

namespace StoreShared.Models
{
    public class RichiestaResetPassword
    {
        public string? Email { get; set; }

        public static implicit operator RichiestaResetPassword(HttpResponseMessage v)
        {
            throw new NotImplementedException();
        }
    }
}
