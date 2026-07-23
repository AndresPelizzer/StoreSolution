using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreShared.Models.StoreDb
{
    [Table("Straordinaria")]
    public class Straordinaria
    {
        [Key]
        public int Codice { get; set; }

        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public int NumeroOre { get; set; }
        public string? Motivo { get; set; }
        public string? Stato { get; set; }

        [Column("CodiceDipendente")]
        public int? CodiceDipendente { get; set; }

        [ForeignKey("CodiceDipendente")]
        public virtual Dipendente? Dipendente { get; set; }
    }
}


