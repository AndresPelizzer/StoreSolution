using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreShared.Models.StoreDb;

[Index("CodiceDipendente", Name = "IX_RichiestaFerie_CodiceDipendente")]
public partial class RichiestaFerie
{
    [Key]
    public int Codice { get; set; }

    public DateTime DataInizio { get; set; }

    public DateTime DataFine { get; set; }

    [StringLength(50)]
    public string? Stato { get; set; }

    public string? Note { get; set; }

    public string? MotivoRifiuto { get; set; }

    public int? CodiceDipendente { get; set; }

    [ForeignKey("CodiceDipendente")]
   
    public virtual Dipendente? Dipendente { get; set; }
}