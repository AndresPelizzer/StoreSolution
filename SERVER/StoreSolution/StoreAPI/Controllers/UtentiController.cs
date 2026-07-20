using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreShared.Models.StoreDb;
using MailKit.Net.Smtp;
using MimeKit;

[ApiController]
[Route("api/[controller]")]
public class UtentiController : ControllerBase
{
    private readonly StoreDbContext _context;
    private readonly IConfiguration _config;
    public UtentiController(StoreDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpGet]
    public async Task<ActionResult<List<Utente>>> GetUtenti()
    {
        try
        {
            return await _context.Utente.ToListAsync();
        }
        catch (Exception ex)
        {
            // log...
            return new List<Utente>();
        }
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<Utente>> GetUtente(int id)
    {


        Utente? utente = await _context.Utente.FirstOrDefaultAsync(u => u.Codice == id);
        if (utente != null)
        {

            return utente;
        }
        else
        {
            return NotFound();
        }


    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Utente>> DeleteUtente(int id)
    {
        Utente? utente = await _context.Utente.FindAsync(id);
        if (utente == null)
        {
            return NotFound();
        }

        _context.Utente.Remove(utente);

        try
        {
            await _context.SaveChangesAsync();
            return Ok(utente);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {

            return Conflict("Impossibile eliminare l'utente perché è associato ad altre entità nel sistema.");
        }
    }



    [HttpPost]

    public async Task<ActionResult<Utente>> AddUtente(Utente utente)
    {


        //await _context.Utente.AddAsync(utente);
        //await _context.SaveChangesAsync();
        //return utente;


        var passwordInChiaro = utente.PasswordHash;
        utente.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordInChiaro);
        await _context.Utente.AddAsync(utente);
        await _context.SaveChangesAsync();

        var cliente = await _context.Cliente.FindAsync(utente.CodiceCliente);
        if (utente.CodiceDipendente == null && utente.CodiceCliente != null)
        {

       
        
            await InviaEmail(cliente!.Email!,
          "Credenziali Store Blazor",
          $"""
            <h3>Benvenuto</h3>
            
            <p>Il tuo account è stato creato con successo.</p>
            <ul>
                <li><strong>Username:</strong> {utente.Username}</li>
                <li><strong>Password:</strong> {passwordInChiaro}</li>
            </ul>
            Clicca sul seguente link per accedere:
            <a href="https://localhost:7035/">Accedi</a>
            
            """
      );
        }



        return utente;


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




    [HttpPut("{id}")]

    public async Task<ActionResult<Utente>> UpdateUtente(Utente utente, int id)
    {
        Utente? utente_da_aggiornare = await _context.Utente.FindAsync(id);
        if (utente_da_aggiornare != null)
        {
            utente_da_aggiornare.Username = utente.Username;
            utente_da_aggiornare.Email = utente.Email;
            utente_da_aggiornare.Ruolo = utente.Ruolo;
            utente_da_aggiornare.PasswordHash = utente.PasswordHash;
            utente_da_aggiornare.CodiceDipendente = utente.CodiceDipendente;
            utente_da_aggiornare.CodiceCliente = utente.CodiceCliente;


            await _context.SaveChangesAsync();
            return utente_da_aggiornare;
        }
        else
        {
            return NotFound();
        }


    }
}


