using System;
using System.Collections.Generic;

namespace FootballBlazor.Shared.Models;

public partial class Giocatori
{
    public int Idgiocatore { get; set; }

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public string? Ruolo { get; set; }

    public int? Idsquadra { get; set; }

    public virtual Squadre? IdsquadraNavigation { get; set; }
}
