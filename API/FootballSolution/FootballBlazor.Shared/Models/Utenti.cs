using System;
using System.ClientModel;
using System.Collections.Generic;

namespace FootballBlazor.Shared.Models;

public partial class Utenti
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Ruolo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Note { get; set; }

    public byte[]? Curriculum { get; set;  }

    public DateTime? DataIscrizione {  get; set; }= DateTime.Now;

}
