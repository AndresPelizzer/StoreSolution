using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreShared.Models.StoreDb;

[Index("CodiceAreaAppl", Name = "IX_Dipendente_CodiceAreaAppl")]
public partial class Dipendente
{
    [Key]
    public int Codice { get; set; }

    [StringLength(100)]
    public string? Nome { get; set; }

    [StringLength(100)]
    public string? Cognome { get; set; }

    [StringLength(255)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? Qualifica { get; set; }

    public bool CapoArea { get; set; }

    public string? Note { get; set; }

    public int? CodiceAreaAppl { get; set; }

    [ForeignKey("CodiceAreaAppl")]
    
    public virtual Area? Area { get; set; }

    
    public virtual ICollection<Richiesta> Richiesta { get; set; } = new List<Richiesta>();



    //// Dipendente.cs - lascia SOLO InverseProperty, togli ForeignKey
    //[InverseProperty("Dipendente")]
    //public virtual ICollection<RichiestaFerie> RichiesteFerie { get; set; } = new List<RichiestaFerie>();


    public virtual ICollection<Utente> Utente { get; set; } = new List<Utente>();
}