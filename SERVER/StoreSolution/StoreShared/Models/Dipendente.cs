using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreShared.Models
{
    public class Dipendente
    {
        [Key]
        public int Codice {  get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public string? Email { get; set; }
        public string? Qualifica {  get; set; }
        public bool CapoArea { get; set; }
        public string? Note { get; set; }

        public int? CodiceAreaAppl { get; set; }
        [ForeignKey("CodiceAreaAppl")]
        public Area? Area { get; set; }


    }
}
