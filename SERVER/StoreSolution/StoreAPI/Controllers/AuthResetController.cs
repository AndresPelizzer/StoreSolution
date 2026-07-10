using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using StoreShared.Models;
using StoreShared.Models.StoreDb;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthResetController : Controller
{

    private readonly StoreDbContext? _db;

    private readonly IConfiguration? _config;


    public AuthResetController(StoreDbContext db, IConfiguration? config)
    {
        _db = db;
        _config = config;
    }

    [HttpPost("richiedi-reset")]

    public async Task<IActionResult> RichiediReset([FromBody] RichiestaResetPassword richiesta)
    {
        var utente = await _db!.Utente.FirstOrDefaultAsync(u => u.Email == richiesta.Email);
        if (utente == null)
        {
            return Ok();
        }

        var token = Guid.NewGuid().ToString("N");
        _db.PasswordResetToken.Add(new PasswordResetToken
        {
            CodiceUtente = utente.Codice,
            Token = token,
            Scadenza = DateTime.UtcNow.AddHours(1),
            Usato = false
        });

        await _db.SaveChangesAsync();

        var link = $"https://localhost:7035/reset-password?token={token}";
        await InviaEmail(
            utente.Email!,
            "Reset Password - Store Blazor",
            $"Clicca qui per resettare la password (valido 1 ora):<br><a href='{link}'>{link}</a>"
        );

        return Ok();

    }


    [HttpPost("conferma-reset")]

    public async Task<IActionResult> ConfermaReset([FromBody]ConfermaResetPassword conferma)
    {
        var record = await _db.PasswordResetToken.FirstOrDefaultAsync(t => t.Token == conferma.Token && !t.Usato);

        if(record==null || record.Scadenza < DateTime.UtcNow)
        {
            return BadRequest("Token non valido o scaduto");
        }
        var utente = await _db.Utente.FindAsync(record.CodiceUtente);
        utente!.PasswordHash= BCrypt.Net.BCrypt.HashPassword(conferma.NuovaPassword);
        record.Usato = true;
        await _db.SaveChangesAsync();

        return Ok();
    }




    private async Task InviaEmail(string destinatario, string oggetto, string corpo)
    {
        var smtp = _config.GetSection("Smtp");
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Store Blazor", smtp["From"]));
        message.To.Add(MailboxAddress.Parse(destinatario));
        message.Subject = oggetto;
        message.Body = new TextPart("html") { Text = corpo };

        using var client = new SmtpClient();
        await client.ConnectAsync(smtp["Host"], int.Parse(smtp["Port"]!), false);
        await client.AuthenticateAsync(smtp["Username"], smtp["Password"]);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
} 




   
