using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreShared.Models.StoreDb;

public partial class Area
{
    [Key]
    public int Codice { get; set; }

    [StringLength(200)]
    public string? Descrizione { get; set; }

    public string? Note { get; set; }

    [InverseProperty("Area")]
    public virtual ICollection<Dipendente> Dipendente { get; set; } = new List<Dipendente>();

    [InverseProperty("Area")]
    public virtual ICollection<Richiesta> Richiesta { get; set; } = new List<Richiesta>();
}
