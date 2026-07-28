using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StoreShared.Models.StoreDb;

[ApiController]
[Route("api/[controller]")]
public class AttDipController : ControllerBase
{
    private readonly StoreDbContext _context;

    public AttDipController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<AttDip>>?> GetAttsDip()
    {
        try
      
        {
            //var test = await _context.AttDip.ToListAsync();
            var result = await _context.AttDip.Include(a=>a.Richiesta).ToListAsync();
            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<AttDip>> GetAttDip(int id)
    {


        AttDip? AttDip = await _context.AttDip.Include(a=>a.Richiesta).FirstOrDefaultAsync(a=> a.Codice == id);
        if (AttDip != null)
        {

            return AttDip;
        }
        else
        {
            return NotFound();
        }
          

    }
    [HttpDelete("{id}")]

    public async Task<ActionResult<AttDip>> DeleteAttDip(int id)
    {
        AttDip? AttDip = await _context.AttDip.FindAsync(id);
        if (AttDip != null)
        {

            _context.AttDip.Remove(AttDip);
            await _context.SaveChangesAsync();
            return Ok(AttDip);
        }
        else
        {
            return NotFound();
        }

    }



    [HttpPost]

    public async Task<ActionResult<AttDip>> AddAttDip(AttDip AttDip)
    {

        AttDip.Richiesta = null;
        await _context.AttDip.AddAsync(AttDip);
        await _context.SaveChangesAsync();
        return AttDip;

    }

    [HttpPut("{id}")]

    public async Task<ActionResult<AttDip>> UpdateAttDip(AttDip AttDip, int id)
    {
        AttDip? AttDip_da_aggiornare = await _context.AttDip.FindAsync(id);
        if (AttDip_da_aggiornare != null)
        {

            AttDip_da_aggiornare.Tipologia = AttDip.Tipologia;

            AttDip_da_aggiornare.Data = AttDip.Data;
            AttDip_da_aggiornare.Note = AttDip.Note;
            AttDip_da_aggiornare.CodiceRichiesta = AttDip.CodiceRichiesta;
            AttDip_da_aggiornare.TempoTotale = AttDip.TempoTotale;

            await _context.SaveChangesAsync();
            return AttDip_da_aggiornare;

        }
   
        else
        {
            return NotFound();
        }





    }



}






