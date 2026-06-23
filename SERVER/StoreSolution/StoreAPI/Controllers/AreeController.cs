using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Data;
using StoreShared.Models;

[ApiController]
[Route("api/[controller]")]
public class AreeController : ControllerBase
{
    private readonly StoreDbContext _context;

    public AreeController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Area>>> GetAree()
    {
        return await _context.Aree.ToListAsync();
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<Area>> GetArea(int id)
    {


        Area? Area = await _context.Aree.FindAsync(id);
        if (Area != null)
        {

            return Area;
        }
        else
        {
            return NotFound();
        }


    }
    [HttpDelete("{id}")]

    public async Task<ActionResult<Area>> DeleteArea(int id)
    {
        Area? Area = await _context.Aree.FindAsync(id);
        if (Area != null)
        {

            _context.Aree.Remove(Area);
            await _context.SaveChangesAsync();
            return Ok(Area);
        }
        else
        {
            return NotFound();
        }

    }



    [HttpPost]

    public async Task<ActionResult<Area>> AddArea(Area Area)
    {
        await _context.Aree.AddAsync(Area);
        await _context.SaveChangesAsync();
        return Area;

    }

    [HttpPut("{id}")]

    public async Task<ActionResult<Area>> UpdateArea(Area Area, int id)
    {
        Area? Area_da_aggiornare = await _context.Aree.FindAsync(id);
        if (Area_da_aggiornare != null)
        {
            Area_da_aggiornare.Descrizione = Area.Descrizione;
            Area_da_aggiornare.Note = Area.Note;
            




            await _context.SaveChangesAsync();
            return Area_da_aggiornare;
        }
        else
        {
            return NotFound();
        }


    }
}
