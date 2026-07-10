using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreShared.Models.StoreDb;

[ApiController]
[Route("api/[controller]")]
public class UtentiController : ControllerBase
{
    private readonly StoreDbContext _context;

    public UtentiController(StoreDbContext context)
    {
        _context = context;
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
        await _context.Utente.AddAsync(utente);
        await _context.SaveChangesAsync();
        return utente;

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


