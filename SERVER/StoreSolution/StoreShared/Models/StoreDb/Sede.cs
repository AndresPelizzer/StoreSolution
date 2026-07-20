using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreShared.Models.StoreDb
{
    public class Sede
    {

        [Key]
        public int Codice {  get; set; }
        public string? Nome { get; set; }

        public string? Citta { get; set; }
    }
}
