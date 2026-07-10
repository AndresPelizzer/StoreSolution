using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreShared.Models.StoreDb;

[Table("PasswordResetToken")]
public partial class PasswordResetToken
{
    [Key]
    public int Codice { get; set; }

    public int CodiceUtente { get; set; }

    [StringLength(200)]
    public string Token { get; set; } = "";

    public DateTime Scadenza { get; set; }

    public bool Usato { get; set; }

    [ForeignKey("CodiceUtente")]
    [InverseProperty("PasswordResetToken")]
    public virtual Utente? Utente { get; set; }
}