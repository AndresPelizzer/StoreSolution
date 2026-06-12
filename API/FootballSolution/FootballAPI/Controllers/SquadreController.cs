using FootballBlazor.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SquadreController : ControllerBase
{
    private readonly CampionatoContext _context;

    public SquadreController(CampionatoContext context)
    {
        _context = context;
    }

    [HttpGet]
    
    public async Task<ActionResult<IEnumerable<Squadre>>> GetSquadre()
    {
        return Ok(await _context.Squadre.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Squadre>> GetSquadra(int id)
    {
        var squadra = await _context.Squadre.FindAsync(id);
        if (squadra == null)
        {
            return NotFound();
    }
        else
        {
            return Ok(squadra);
        }
}

    [HttpPost]
    public async Task<ActionResult<Squadre>> PostSquadra(Squadre squadra)
    {
        //_context.Squadre.Add(squadra);
        await _context.SaveChangesAsync();
        return Ok();
    
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutSquadra(int id, Squadre squadra)
    {
        if(id == squadra.Idsquadra)
        {
            _context.Entry(squadra).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSquadra(int id)
    {
        var squadra = await _context.Squadre.FindAsync(id);
        if(squadra == null)
        {
            return NotFound();
        }
        else
        {
            _context.Squadre.Remove(squadra);
            await _context.SaveChangesAsync();
            return Ok();
                
        }
    }
}