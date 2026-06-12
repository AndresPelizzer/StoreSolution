using System;
using System.Collections.Generic;

namespace FootballBlazor.Shared.Models;

public partial class VistaSquadre
{
    public int Idsquadra { get; set; }

    public string NomeSquadra { get; set; } = null!;

    public string? Allenatore { get; set; }

    public string Citta { get; set; } = null!;

    public int? NumeroGiocatoriInRosa { get; set; }

    public int? NumeroGiocatori { get; set; }
}
