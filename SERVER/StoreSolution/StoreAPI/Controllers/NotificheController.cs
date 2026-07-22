using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StoreShared.Models.StoreDb;

namespace StoreAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class NotificheController : ControllerBase
{
    
    private readonly StoreDbContext _context;

   public NotificheController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]



    
    public async Task<ActionResult<List<Notifica>>> GetNotifiche(int id)
    {

       
        return await _context.Notifica.Where(n => n.CodiceCliente == id && !n.Letta).ToListAsync();
    }


    [HttpGet("dipendente/{id}")]
    public async Task<ActionResult<List<Notifica>>> GetNotificheCapoArea(int id)
    {
        return await _context.Notifica.Where(n => n.CodiceDipendente == id && !n.Letta).ToListAsync();
    }

    [HttpPost]

    public async Task<ActionResult<Notifica>> AddNotifica(Notifica notifica)
    {
        notifica.DataCreazione = DateTime.Now;
        notifica.Letta = false;
        await _context.Notifica.AddAsync(notifica);
        await _context.SaveChangesAsync();
        return Ok(notifica);
        
    }

    [HttpPut("{id}/letta")]

    public async Task<ActionResult<Notifica>> SegnaComeLetta(int id)
    {
        Notifica? notifica = await _context.Notifica.FindAsync(id);
        if (notifica != null)
        {
            notifica.Letta = true;
            await _context.SaveChangesAsync();
            return notifica;
        }
        return NotFound();
    }
}



