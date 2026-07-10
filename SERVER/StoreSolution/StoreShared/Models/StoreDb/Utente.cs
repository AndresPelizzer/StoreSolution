using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreShared.Models.StoreDb;

[Index("CodiceCliente", Name = "IX_Utente_CodiceCliente")]
[Index("CodiceDipendente", Name = "IX_Utente_CodiceDipendente")]
public partial class Utente
{
    [Key]
    public int Codice { get; set; }

    [StringLength(100)]
    public string? Username { get; set; }

    [StringLength(255)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? Ruolo { get; set; }

    [StringLength(255)]
    public string? PasswordHash { get; set; }

    public int? CodiceDipendente { get; set; }

    public int? CodiceCliente { get; set; }

    [ForeignKey("CodiceCliente")]
    [InverseProperty("Utente")]
    public virtual Cliente? CodiceClienteNavigation { get; set; }

    [ForeignKey("CodiceDipendente")]
    [InverseProperty("Utente")]
    public virtual Dipendente? CodiceDipendenteNavigation { get; set; }

    [InverseProperty("Utente")]
    public virtual ICollection<PasswordResetToken> PasswordResetToken { get; set; } = new List<PasswordResetToken>();
}
