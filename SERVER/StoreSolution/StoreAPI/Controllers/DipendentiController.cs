using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Hubs;
using StoreShared.Models.StoreDb;

[ApiController]
[Route("api/[controller]")]
public class DipendentiController : ControllerBase
{
    private readonly StoreDbContext _context;
    private readonly IHubContext<StoreHub> _hub;

    public DipendentiController(StoreDbContext context, IHubContext<StoreHub> hub)
    {
        _context = context;
        _hub = hub;
    }

    [HttpGet]
    public async Task<ActionResult<List<Dipendente>>> GetDipendenti()
    {
        return await _context.Dipendente.Include(d => d.Area).ToListAsync();
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<Dipendente>> GetDipendente(int id) {


        Dipendente? dipendente = await _context.Dipendente.Include(d => d.Area).FirstOrDefaultAsync(d => d.Codice == id);
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
        Dipendente? dipendente = await _context.Dipendente.FindAsync(id);
        if (dipendente == null)
        {
            return NotFound();
        }

        _context.Dipendente.Remove(dipendente);

        try
        {
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("AggiornaDipendenti");
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
        if (dipendente.CapoArea)
        {
            bool giapresente = await _context.Dipendente.AnyAsync(d => d.CapoArea == true && d.CodiceAreaAppl == dipendente.CodiceAreaAppl);
            if (giapresente)
            {
                return BadRequest("Un dipendente capo area é gia presente in questa area");
            }
        }
        await _context.Dipendente.AddAsync(dipendente);
        await _context.SaveChangesAsync();
        await _hub.Clients.All.SendAsync("AggiornaDipendenti");
        return dipendente;

    }

    [HttpPut("{id}")]

    public async Task<ActionResult<Dipendente>> UpdateDipendente(Dipendente dipendente, int id)
    {

        if (dipendente.CapoArea)
        {
            bool giapresente = await _context.Dipendente.AnyAsync(d => d.CodiceAreaAppl == dipendente.CodiceAreaAppl && d.CapoArea == true && d.Codice != id);
            if (giapresente)
            {
                return BadRequest("Dipendente capo area già presente!!!");
            }
        }
       Dipendente? dipendente_da_aggiornare =await _context.Dipendente.FindAsync(id);
        if (dipendente_da_aggiornare != null) {
            dipendente_da_aggiornare.Nome = dipendente.Nome;
            dipendente_da_aggiornare.Cognome = dipendente.Cognome;
            dipendente_da_aggiornare.Email = dipendente.Email;
            dipendente_da_aggiornare.Qualifica= dipendente.Qualifica;
            dipendente_da_aggiornare.CapoArea= dipendente.CapoArea;
            dipendente_da_aggiornare.CodiceAreaAppl = dipendente.CodiceAreaAppl;
           

            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("AggiornaDipendenti");
            return dipendente_da_aggiornare;
        }
        else
        {
            return NotFound();
        }
    
         
    }
}
