using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreShared.Models
{
    public class Richiesta
    {

        [Key]
        public int Codice { get; set; }
        public string? Titolo { get; set; }

        public string? Stato { get; set; }
        public string? Descrizione { get; set; }
        public DateTime DataRichiesta {  get; set; }


        public int? CodiceDipendente { get; set; }
        [ForeignKey("CodiceDipendente")]
        public Dipendente? Dipendente { get; set; }

        public int? CodiceArea { get; set; }
        [ForeignKey("CodiceArea")]
        public Area? Area { get; set; }


        public int? CodiceCliente {  get; set; }
        [ForeignKey("CodiceCliente")]
        public Cliente? Cliente { get; set; }





 
    }
}
