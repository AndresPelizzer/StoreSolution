using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Data;
using StoreShared.Models;

[ApiController]
[Route("api/[controller]")]
public class DipendentiController : ControllerBase
{
    private readonly StoreDbContext _context;

    public DipendentiController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Dipendente>>> GetDipendenti()
    {
        return await _context.Dipendenti.Include(d => d.Area).ToListAsync();
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<Dipendente>> GetDipendente(int id) {


        Dipendente? dipendente = await _context.Dipendenti.Include(d => d.Area).FirstOrDefaultAsync(d => d.Codice == id);
        if (dipendente != null)
        {

            return dipendente;
        }
        else
        {
            return NotFound();
        }
        

}
    

    [HttpDelete("{id}!")]
    public async Task<ActionResult<Dipendente>> DeleteDipendente(int id)
    {
        Dipendente? dipendente = await _context.Dipendenti.FindAsync(id);
        if (dipendente == null)
        {
            return NotFound();
        }

        _context.Dipendenti.Remove(dipendente);

        try
        {
            await _context.SaveChangesAsync();
            return Ok(dipendente);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
           
            return Conflict("Impossibile eliminare il dipendente perché è associato ad altre entità nel sistema.");
        }
    }



    [HttpPost]

    public async Task<ActionResult<Dipendente>> AddDipendente(Dipendente dipendente)
    {
        await _context.Dipendenti.AddAsync(dipendente);
        await _context.SaveChangesAsync();
        return dipendente;

    }

    [HttpPut("{id}")]

    public async Task<ActionResult<Dipendente>> UpdateDipendente(Dipendente dipendente, int id)
    {
       Dipendente? dipendente_da_aggiornare =await _context.Dipendenti.FindAsync(id);
        if (dipendente_da_aggiornare != null) {
            dipendente_da_aggiornare.Nome = dipendente.Nome;
            dipendente_da_aggiornare.Cognome = dipendente.Cognome;
            dipendente_da_aggiornare.Email = dipendente.Email;
            dipendente_da_aggiornare.Qualifica= dipendente.Qualifica;
            dipendente_da_aggiornare.CapoArea= dipendente.CapoArea;
            dipendente_da_aggiornare.CodiceAreaAppl = dipendente.CodiceAreaAppl;
           

            await _context.SaveChangesAsync();
            return dipendente_da_aggiornare;
        }
        else
        {
            return NotFound();
        }
    
         
    }
}
