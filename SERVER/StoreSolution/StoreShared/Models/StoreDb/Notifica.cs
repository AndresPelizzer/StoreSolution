using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreShared.Models.StoreDb;

public partial class Notifica
{
    [Key]
    public int Codice { get; set; }

    public string? Messaggio { get; set; }

    public bool Letta { get; set; }

    public DateTime DataCreazione { get; set; }

    public int? CodiceCliente { get; set; }

    [ForeignKey("CodiceCliente")]
   
    public virtual Cliente? Cliente { get; set; }

    
    public int? CodiceDipendente { get; set; }

    [ForeignKey("CodiceDipendente")]
  
    public virtual Dipendente? Dipendente { get; set; }
}

