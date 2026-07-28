using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreShared.Models.StoreDb
{
    public class AttDip
    {

        public int Codice { get; set; }
        public DateTime Data { get; set; }
        public string? Tipologia { get; set; }

        public string? Note { get; set; }

        public TimeSpan TempoTotale { get; set; }

        [Column("CodiceRichiesta")]
        public int CodiceRichiesta { get; set; }


        [ForeignKey("CodiceRichiesta")]
        public virtual Richiesta? Richiesta { get; set; }
    }
}
