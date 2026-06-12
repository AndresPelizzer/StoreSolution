using FootballBlazor.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class GiocatoriController : ControllerBase
{
    private readonly CampionatoContext _context;

    public GiocatoriController(CampionatoContext context)
    {
        _context = context;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<Giocatori>>> GetGiocatori()
    {
        return Ok(await _context.Giocatori.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Giocatori>> GetGiocatore(int id)
    {
        var giocatore = await _context.Giocatori.FindAsync(id);
        if (giocatore == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(giocatore);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Giocatori>> PostGiocatore(Giocatori giocatore)
    {
        _context.Giocatori.Add(giocatore);
        await _context.SaveChangesAsync();
        return Ok();

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutGiocatore(int id, Giocatori giocatore)
    {
        if (id == giocatore.Idgiocatore)
        {
            _context.Entry(giocatore).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGiocatore(int id)
    {
        var giocatore = await _context.Giocatori.FindAsync(id);
        if (giocatore == null)
        {
            return NotFound();
        }
        else
        {
            _context.Giocatori.Remove(giocatore);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
