using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Data;
using StoreShared;

[ApiController]
[Route("api/[controller]")]
public class RichiesteController : ControllerBase
{
    private readonly StoreDbContext _context;

    public RichiesteController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Richiesta>>> GetRichieste()
    {
        return await _context.Richieste.Include(r => r.Area).Include(r=>r.Cliente).Include(r=>r.Dipendente).ToListAsync();
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<Richiesta>> GetRichiesta(int id)
    {


        Richiesta? Richiesta = await _context.Richieste.Include(r => r.Area).Include(r => r.Cliente).Include(r => r.Dipendente).FirstOrDefaultAsync(r => r.Codice == id);
        if (Richiesta != null)
        {

            return Richiesta;
        }
        else
        {
            return NotFound();
        }


    }
    [HttpDelete("{id}")]

    public async Task<ActionResult<Richiesta>> DeleteRichiesta(int id)
    {
        Richiesta? Richiesta = await _context.Richieste.FindAsync(id);
        if (Richiesta != null)
        {

            _context.Richieste.Remove(Richiesta);
            await _context.SaveChangesAsync();
            return Ok(Richiesta);
        }
        else
        {
            return NotFound();
        }

    }



    [HttpPost]

    public async Task<ActionResult<Richiesta>> AddRichiesta(Richiesta Richiesta)
    {
        await _context.Richieste.AddAsync(Richiesta);
        await _context.SaveChangesAsync();
        return Richiesta;

    }

    [HttpPut("{id}")]

    public async Task<ActionResult<Richiesta>> UpdateRichiesta(Richiesta Richiesta, int id)
    {
        Richiesta? Richiesta_da_aggiornare = await _context.Richieste.FindAsync(id);
        if (Richiesta_da_aggiornare != null)
        {
            Richiesta_da_aggiornare.Titolo= Richiesta.Titolo;
            Richiesta_da_aggiornare.Stato = Richiesta.Stato;
            Richiesta_da_aggiornare.Descrizione = Richiesta.Descrizione;
            Richiesta_da_aggiornare.DataRichiesta = Richiesta.DataRichiesta;
            Richiesta_da_aggiornare.CodiceDipendente = Richiesta.CodiceDipendente;
            Richiesta_da_aggiornare.CodiceCliente= Richiesta.CodiceCliente;

            Richiesta_da_aggiornare.CodiceArea = Richiesta.CodiceArea;

            await _context.SaveChangesAsync();
            return Richiesta_da_aggiornare;
        }
        else
        {
            return NotFound();
        }


    }
}
