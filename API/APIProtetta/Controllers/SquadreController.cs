using FootballBlazor.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]

[Authorize]
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
        try
        {

            var identity = HttpContext.User.Identity;
            var claims = HttpContext.User.Claims;

            var roles = claims.Where(c => c.Type == ClaimTypes.Role);
            string role=string.Empty;
            if (roles.Any()){
                role = roles.First().Value;
            }

            var result = await _context.Squadre
               .Include(s => s.Giocatori)
               .ToListAsync();



            if(role != "amminstratore")
            {
                result = result.Where(squadra => squadra.Giocatori.Count>0).ToList();
            }

            return Ok(result);

        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return null;
        }
    }


    [HttpGet("vista")]
    public async Task<ActionResult<IEnumerable<Squadre>>> GetVistaSquadre()
    {
        try
        {
            var result = await _context.VistaSquadre.ToListAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return null;
        }
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<Squadre>> GetSquadra(int id)
    {

        try
        {
            var squadra = await _context.Squadre
                .Include(s => s.Giocatori)
                .FirstOrDefaultAsync(s => s.Idsquadra == id);

            if (squadra == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(squadra);
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return null;
        }
    }

    [HttpPost]
    public async Task<ActionResult<Squadre>> PostSquadra(Squadre squadra)
    {
        _context.Squadre.Add(squadra);
        await _context.SaveChangesAsync();
        return Ok();

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutSquadra(int id, Squadre squadra)
    {
        if (id == squadra.Idsquadra)
        {
            _context.Entry(squadra).State = EntityState.Modified;
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
        if (squadra == null)
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