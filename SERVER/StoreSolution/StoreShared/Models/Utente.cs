using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreShared.Models
{
    public class Utente
    {
        [Key]
        public int Codice { get; set; }
        public string? Username { get; set; }

        public string? Email { get; set; }
        public string? Ruolo { get; set; }
        public string? PasswordHash { get; set; }
        public int? CodiceDipendente { get; set; }
        public int? CodiceCliente { get; set; }

    }
}
