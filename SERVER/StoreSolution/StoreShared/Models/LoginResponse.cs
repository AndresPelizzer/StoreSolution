using System;
using System.Collections.Generic;
using System.Text;

namespace StoreShared.Models
{
    public class LoginResponse
    {

        public string? Token{ get; set; }
        public string? Ruolo {  get; set; }
        public int? CodiceUtente { get; set; }

    }
}
