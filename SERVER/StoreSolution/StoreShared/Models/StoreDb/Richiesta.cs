using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreShared.Models.StoreDb;

[Index("CodiceArea", Name = "IX_Richiesta_CodiceArea")]
[Index("CodiceCliente", Name = "IX_Richiesta_CodiceCliente")]
[Index("CodiceDipendente", Name = "IX_Richiesta_CodiceDipendente")]
public partial class Richiesta
{
    [Key]
    public int Codice { get; set; }

    [StringLength(200)]
    public string? Titolo { get; set; }

    [StringLength(50)]
    public string? Stato { get; set; }

    public string? Descrizione { get; set; }

    public DateTime DataRichiesta { get; set; }


    public string? Allegato { get; set; }

    public int? CodiceDipendente { get; set; }

    public int? CodiceArea { get; set; }

    public int? CodiceCliente { get; set; }

    [ForeignKey("CodiceArea")]
    [InverseProperty("Richiesta")]
    public virtual Area? Area { get; set; }

    [ForeignKey("CodiceCliente")]
    [InverseProperty("Richiesta")]
    public virtual Cliente? Cliente { get; set; }

    [ForeignKey("CodiceDipendente")]
    [InverseProperty("Richiesta")]
    public virtual Dipendente? Dipendente { get; set; }
}
