using System;
using System.Collections.Generic;

namespace FootballBlazor.Shared.Models;

public partial class Squadre
{
    public int Idsquadra { get; set; }

    public string NomeSquadra { get; set; } = null!;

    public string Citta { get; set; } = null!;

    public string? Allenatore { get; set; }

    public int? NumeroGiocatoriInRosa { get; set; }

    public virtual ICollection<Giocatori> Giocatori { get; set; } = new List<Giocatori>();
}
