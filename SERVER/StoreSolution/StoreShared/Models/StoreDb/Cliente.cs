using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreShared.Models.StoreDb;

public partial class Cliente
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
    public string? Settore { get; set; }

    [StringLength(20)]
    public string? PartitaIva { get; set; }

    [InverseProperty("Cliente")]
    public virtual ICollection<Richiesta> Richiesta { get; set; } = new List<Richiesta>();

    [InverseProperty("CodiceClienteNavigation")]
    public virtual ICollection<Utente> Utente { get; set; } = new List<Utente>();


    [InverseProperty("Cliente")]

    public virtual ICollection<Notifica> Notifica { get; set; }= new List<Notifica>();
}
