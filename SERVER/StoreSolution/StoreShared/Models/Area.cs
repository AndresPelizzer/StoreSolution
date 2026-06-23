using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreShared.Models
{
    public class Area
    {


        [Key]
        public int? Codice { get; set; }
        public string? Descrizione { get; set; }
        public string? Note {  get; set; }

    }
}
